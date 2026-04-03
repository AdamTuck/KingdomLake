using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerInput input;

    public PlayerState(PlayerController _player)
    {
        this.player = _player;
        this.input = player.GetInput();
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}