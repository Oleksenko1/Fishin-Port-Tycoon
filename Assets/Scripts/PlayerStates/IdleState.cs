using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
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
