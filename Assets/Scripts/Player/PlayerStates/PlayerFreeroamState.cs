using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeroamState : PlayerState
{
    private float cameraXRotation;
    private Vector3 playerVelocity;
    private Vector3 startPosition;
    bool slideToPos;
    bool resetPos;

    private iInteractable interactable;

    // Player visuals will bounce as you move
    [Header("Bounce Tuning")]
    private GameObject playerVisual;
    private const float baseBounceDuration = 0.35f;   // hop duration at speedTier1 (baseline "little hop")
    private const float bounceHeight = 0.12f;   // peak hop height in world units — constant regardless of speed

    private float bounceTimer;
    private float currentBounceDuration;   // computed per-cycle from movement speed at the moment the hop starts
    private bool isBouncing;

    // Player will start at one speed and then move into a modified speed after a short interval to give the sense of ramping up 
    [Header("Move Speed Tuning")]
    private const float speedTier1 = 1.0f;        // multiplier at low/baseline speed
    private const float speedTier2 = 1.6f;        // multiplier once fully ramped up
    private const float timeToRamp = 1.5f;        // seconds of sustained input needed to go from Tier 1 -> Tier 2
    private const float rampDecaySpeed = 2.0f;    // how quickly the ramp falls back off once input stops

    private float inputHeldTime;
    private float currentSpeedMultiplier;

    //Variables handling the hold-charge for cast strength later
    [Header("Cast Settings")]
    public int maxValue = 100;          // counter wraps to 0 once it would reach this
    public float tickInterval = 0.2f;  // seconds between increment
    private int currentCast;
    private Coroutine castRoutine;



    public PlayerFreeroamState(PlayerController _player, Vector3 startPos) : base(_player)
    {
        startPosition = startPos;
    }

    public override void OnStateEnter()
    {
        UIManager.instance.LockCursor();
        PlayerInput.GetInstance().InputsUnlocked();
        
        PlayerController.instance.GetCharacterController().enabled = false;
        player.transform.position = startPosition;
        PlayerController.instance.GetCharacterController().enabled = true;

        CameraManager.instance.EnableOverheadCam();

        // Pull the decoupled visual reference (assumes PlayerController exposes GetPlayerVisual())
        playerVisual = player.GetPlayerVisual();

        currentSpeedMultiplier = speedTier1;
        inputHeldTime = 0f;
        isBouncing = false;
    }

    public override void OnStateExit()
    {
        UIManager.instance.UnlockCursor();

        // Reset bounce visual offset so it doesn't persist into other states
        if (playerVisual != null)
        {
            Vector3 localPos = playerVisual.transform.localPosition;
            localPos.y = 0f;
            playerVisual.transform.localPosition = localPos;
        }
    }

    public override void OnStateUpdate()
    {
        /*if (slideToPos)
        {
            input.InputsLocked();
            SlideIntoPos();

            if (Vector3.Distance(player.transform.position, startPosition) < 0.1)
            {
                input.InputsUnlocked();
                slideToPos = false;
            }
        }
        else
        {*/
        
        MovePlayer();
        UpdateBounce();
        //CameraRotation();
        CheckInputs();
        //}
    }

    private void SlideIntoPos()
    {
        //player.transform.position = Vector3.Lerp(player.transform.position, startPosition, player.playerAnimateMoveSpeed * Time.deltaTime);
        //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, startPosition, player.playerAnimateMoveSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        bool hasInput = input.vertical != 0f || input.horizontal != 0f;

        // --- Speed ramp: Tier 1 -> Tier 2 over 'timeToRamp' seconds of sustained input, decays when input stops ---
        if (hasInput)
            inputHeldTime += Time.deltaTime;
        else
            inputHeldTime -= Time.deltaTime * rampDecaySpeed;

        inputHeldTime = Mathf.Clamp(inputHeldTime, 0f, timeToRamp);

        float rampT = inputHeldTime / timeToRamp;
        currentSpeedMultiplier = Mathf.Lerp(speedTier1, speedTier2, rampT);

        Vector3 movementDirection = (player.transform.forward * input.vertical + player.transform.right * input.horizontal) * (player.GetWalkSpeed() * currentSpeedMultiplier) * Time.deltaTime;
        player.GetCharacterController().Move(movementDirection);

        // Trigger a bounce cycle when starting to move while not already mid-bounce
        if (movementDirection != Vector3.zero && !isBouncing)
            StartBounce();

        // Rotate Player
        if (movementDirection != Vector3.zero)
            player.transform.Find("PlayerModel").transform.rotation = Quaternion.Slerp(player.transform.Find("PlayerModel").transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * 40f);

        // Fall if not grounded
        if (player.IsGrounded() && playerVelocity.y < 0)
            playerVelocity.y = -2.0f;

        playerVelocity.y += PlayerController.instance.GetGravity() * Time.deltaTime;

        player.GetCharacterController().Move(playerVelocity * Time.deltaTime);
    }

    void StartBounce()
    {
        isBouncing = true;
        bounceTimer = 0f;

        // Inverse relationship between movement speed and hop SPEED (not height):
        // faster movement -> longer bounce duration -> slower-looking, more graceful leap.
        // Locked in at the start of the cycle so a single hop's timing doesn't warp mid-arc
        // if speed changes while airborne.
        currentBounceDuration = baseBounceDuration * (currentSpeedMultiplier / speedTier1);
    }
    void UpdateBounce()
    {
        if (!isBouncing || playerVisual == null)
            return;

        bounceTimer += Time.deltaTime;
        float t = bounceTimer / currentBounceDuration;

        if (t >= 1f)
        {
            isBouncing = false;

            Vector3 resetPos = playerVisual.transform.localPosition;
            resetPos.y = 0f;
            playerVisual.transform.localPosition = resetPos;

            // Chain immediately into the next hop if still moving
            bool stillMoving = input.vertical != 0f || input.horizontal != 0f;
            if (stillMoving)
                StartBounce();

            return;
        }

        // Parabolic arc: 0 at t=0, peak (bounceHeight) at t=0.5, 0 at t=1.
        // Height is independent of currentBounceDuration, so it stays constant
        // regardless of movement speed.
        float arc = 4f * bounceHeight * t * (1f - t);

        Vector3 localPos = playerVisual.transform.localPosition;
        localPos.y = arc;
        playerVisual.transform.localPosition = localPos;
    }

    void GroundedCheck()
    {
        player.SetGrounded(Physics.CheckSphere(player.GetGroundCheckTransform().position, player.GetGroundCheckDistance(), player.GetGroundLayerMask()));
    }

    void CameraRotation()
    {
        cameraXRotation += input.mouseY * Time.deltaTime * player.GetTurnSpeed() * (player.GetInvertMouse() ? 1 : -1);
        cameraXRotation = Mathf.Clamp(cameraXRotation, -40, 40);

        player.GetMainCamera().transform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
    }

    private void CheckInputs()
    {

        if (PlayerInput.GetInstance().escape)
        {
            UIManager.instance.PauseGameToggle();
        }

        if (PlayerInput.GetInstance().interactSecondary)
        {
            player.ChangeState(new PlayerFishingState(player));
        }

        if (PlayerInput.GetInstance().leftBtnDown && castRoutine == null)
        {
            Debug.Log("Player Cast Button Pressed:");
            currentCast = 0;
            castRoutine = player.StartCoroutine(CastLoop());
        }

        if (PlayerInput.GetInstance().leftBtnUp && castRoutine != null)
        {
            player.StopCoroutine(castRoutine);
            castRoutine = null;
            HandleFinalCast(currentCast);
        }

        if (PlayerInput.GetInstance().interact)
        {
            //Benoit: Later, I think we should break out the free roam state into
            //"in town" vs "not in town" so that interact can do double duty as
            //the cast lure button
            interactable = PlayerController.instance.GetCurrentTrigger().GetComponent<iInteractable>();

            if (interactable != null)
                interactable.OnInteract();
        }



        /*if (PlayerInput.GetInstance().space && !GameManager.instance.IsLockedAtDesk())
        {
            if (GameManager.instance.GetCurrentGameState() == "DayExecuting")
            {

            }
        }
        else if (PlayerInput.GetInstance().tab && !GameManager.instance.IsLockedAtDesk())
        {
            player.ChangeState(new PlayerDeskState(player));
        }
        else if (PlayerInput.GetInstance().interactSecondary)
        {
            SoundManager.instance.PlayMusicToggle();
        }*/
    }

    private IEnumerator CastLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);

            currentCast++;
            Debug.Log("iterating " + currentCast);

            if (currentCast >= maxValue)
                currentCast = 0;
        }
    }

    private void HandleFinalCast(int finalValue)
    {
        Debug.Log($"Final cast on release: {finalValue}");
    }

}
