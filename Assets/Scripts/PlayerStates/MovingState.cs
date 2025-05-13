using System;
using UnityEngine;

public class MovingState : PlayerState
{
    public override void OnEnter()
    {
        Debug.Log($"Entered MovingState");
    }

    public override void OnExit()
    {
        Debug.Log($"Exit from MovingState");
    }

    public override void OnUpdate()
    {
        if (playerController.GetPlayerMovement().GetInputVector() != Vector2.zero)
        {
            playerController.MovePlayer();
        }
        else
        {
            OnExit();

            stateController.EnterState(stateController.idleState);
        }
    }
}