using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingState : PlayerState
{
    [SerializeField] private FishingController fishingController;

    public FishingState(PlayerController playerController, PlayerStateController stateController, FishingController fishingController) : base(playerController, stateController)
    {
        this.fishingController = fishingController;
    }

    public override void InitializeState()
    {
        fishingController.OnExit += ExitFishing;
    }
    public override void OnEnter()
    {
        fishingController.StartFishing();

        playerController.GetPlayerMovement().EnableJoystick(false);
    }
    private void ExitFishing()
    {
        stateController.EnterState(stateController.idleState);
    }

    public override void OnExit()
    {
        playerController.GetPlayerMovement().EnableJoystick(true);
    }

    public override void OnUpdate() { }
}
