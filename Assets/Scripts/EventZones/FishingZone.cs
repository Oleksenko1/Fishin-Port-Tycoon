using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingZone : EventZone
{
    [SerializeField] private PlayerStateController stateController;
    public override void OnPlayerEnter()
    {
        stateController.EnterState(stateController.fishingState);
    }
}
