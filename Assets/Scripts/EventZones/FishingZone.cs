using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class FishingZone : EventZone
{
    [Inject] private PlayerStateController stateController;
    [Inject] private PlayerInventory playerInventory;
    public override void OnPlayerEnter()
    {
        if (!playerInventory.HasSpace())
        {
            UIWarningPopup.Instance.ShowWarning("You have no space left in inventory!", 4f);
            return;
        }
        stateController.EnterState(stateController.fishingState);
    }
}
