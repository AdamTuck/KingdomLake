using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeroamState : PlayerState
{
    private float cameraXRotation;
    private Vector3 playerVelocity;
    bool slideToPos;
    bool resetPos;

    public PlayerFreeroamState(PlayerController _player) : base(_player)
    {
    }

    public override void OnStateEnter()
    {
        //player.GetMainCamera().transform.localRotation = Quaternion.Euler(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerInput.GetInstance().InputsUnlocked();
        //UIManager.instance.EnableCrosshair();
        //slideToPos = true;
    }

    public override void OnStateExit()
    {
        player.GetMainCamera().transform.localRotation = Quaternion.Euler(0, 0, 0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //UIManager.instance.DisableCrosshair();
    }

    public override void OnStateUpdate()
    {
        /*if (slideToPos)
        {
            input.InputsLocked();
            SlideIntoPos();

            if (Vector3.Distance(player.transform.position, player.freeRoamStart.position) < 0.1)
            {
                input.InputsUnlocked();
                slideToPos = false;
            }
        }
        else
        {*/
            MovePlayer();
            RotatePlayer();
            //CameraRotation();
            CheckInputs();
        //}
    }

    private void SlideIntoPos()
    {
        //player.transform.position = Vector3.Lerp(player.transform.position, player.freeRoamStart.position, player.playerAnimateMoveSpeed * Time.deltaTime);
        //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, player.freeRoamStart.rotation, player.playerAnimateMoveSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        player.GetCharacterController().Move((player.transform.forward * input.vertical + player.transform.right * input.horizontal) * player.GetWalkSpeed() * Time.deltaTime);

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

    void RotatePlayer()
    {
        player.transform.Rotate(Vector3.up * player.GetTurnSpeed() * Time.deltaTime * input.mouseX);
    }
}