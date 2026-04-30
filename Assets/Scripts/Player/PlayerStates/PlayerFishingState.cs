using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishingState : PlayerState
{
    public PlayerFishingState(PlayerController _player) : base(_player)
    {
    }

    public override void OnStateEnter()
    {
        UIManager.instance.LockCursor();
        PlayerInput.GetInstance().InputsUnlocked();

        UIManager.instance.EnableFishingUI(true);
        CameraManager.instance.EnableFishingCam();
    }

    public override void OnStateExit()
    {
        UIManager.instance.EnableFishingUI(false);
    }

    public override void OnStateUpdate()
    {

        CheckInputs();
    }

    private void CheckInputs()
    {
        if (PlayerInput.GetInstance().escape)
        {
            UIManager.instance.PauseGameToggle();
        }

        if (PlayerInput.GetInstance().interactSecondary)
        {
            player.ChangeState(new PlayerFreeroamState(player, player.transform.position));
        }
    }
}
