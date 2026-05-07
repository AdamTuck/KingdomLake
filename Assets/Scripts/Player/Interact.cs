using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    protected PlayerInput playerInput;

    private void Start()
    {
        playerInput = PlayerInput.GetInstance();
    }

    void Update()
    {
        Interaction();
    }

    public abstract void Interaction();
}