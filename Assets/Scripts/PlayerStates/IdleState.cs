using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void OnEnter()
    {
        Debug.Log("Entered IdleState");
    }

    public override void OnExit()
    {
        Debug.Log("Exit from IdleState");
    }

    public override void OnUpdate()
    {
        // Check if player is trying to move
        if (playerController.GetPlayerMovement().GetInputVector() != Vector2.zero)
        {
            stateController.EnterState(stateController.movingState);
        }
    }
}
