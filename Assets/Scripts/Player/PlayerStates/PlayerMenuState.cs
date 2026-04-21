using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuState : PlayerState
{

    public PlayerMenuState(PlayerController _player) : base(_player)
    {
    }

    public override void OnStateEnter()
    {
        UIManager.instance.UnlockCursor();
    }

    public override void OnStateExit()
    {
        UIManager.instance.LockCursor();
    }

    public override void OnStateUpdate()
    {
        //MovePlayer();
        //RotatePlayer();
        //CheckInputs();
    }
}