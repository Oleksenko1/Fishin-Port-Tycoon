using System;
using UnityEngine;

public class MovingState : PlayerState
{
    public MovingState(PlayerController playerController, PlayerStateController stateController) : base(playerController, stateController) {}

    public override void OnEnter() {}

    public override void OnExit() {}
    public override void OnUpdate()
    {
        if (playerController.GetPlayerMovement().GetInputVector() != Vector2.zero)
        {
            playerController.MovePlayer();
        }
        else
        {
            stateController.EnterState(stateController.idleState);
        }
    }
}