using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class FishingZone : EventZone
{
    [Inject] private PlayerStateController stateController;
    public override void OnPlayerEnter()
    {
        stateController.EnterState(stateController.fishingState);
    }
}
