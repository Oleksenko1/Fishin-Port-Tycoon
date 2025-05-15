using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingState : PlayerState
{
    [SerializeField] private FishingController fishingController;
    public override void OnInitializeState()
    {
        fishingController.OnExit += ExitFishing;
    }
    public override void OnEnter()
    {
        Debug.Log("Entered fishing state");

        fishingController.StartFishing();

        playerController.GetPlayerMovement().EnableJoystick(false);
    }
    private void ExitFishing()
    {
        stateController.EnterState(stateController.idleState);
    }

    public override void OnExit()
    {
        Debug.Log("Exit from fishing state");

        playerController.GetPlayerMovement().EnableJoystick(true);
    }

    public override void OnUpdate()
    {

    }
}
