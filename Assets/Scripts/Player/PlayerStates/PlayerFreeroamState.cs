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

    public PlayerFreeroamState(PlayerController _player, Vector3 startPos) : base(_player)
    {
        startPosition = startPos;
    }

    public override void OnStateEnter()
    {
        PlayerInput.GetInstance().InputsUnlocked();

        //player.GetMainCamera().transform.localRotation = Quaternion.Euler(0, 0, 0);
        //slideToPos = true;
        PlayerController.instance.GetCharacterController().enabled = false;
        player.transform.position = startPosition;
        PlayerController.instance.GetCharacterController().enabled = true;
    }

    public override void OnStateExit()
    {
        //player.GetMainCamera().transform.localRotation = Quaternion.Euler(0, 0, 0);
        UIManager.instance.UnlockCursor();
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
        Vector3 movementDirection = (player.transform.forward * input.vertical + player.transform.right * input.horizontal) * player.GetWalkSpeed() * Time.deltaTime;
        player.GetCharacterController().Move(movementDirection);

        // Rotate Player
        if (movementDirection != Vector3.zero)
            player.transform.Find("PlayerModel").transform.rotation = Quaternion.Slerp(player.transform.Find("PlayerModel").transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * 40f);

        // Fall if not grounded
        if (player.IsGrounded() && playerVelocity.y < 0)
            playerVelocity.y = -2.0f;

        playerVelocity.y += PlayerController.instance.GetGravity() * Time.deltaTime;

        player.GetCharacterController().Move(playerVelocity * Time.deltaTime);
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
}