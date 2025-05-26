using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class CookingSpotOutputZone : EventZone
{
    [Inject] private CookingSpot cookingSpot;
    [Inject] private PlayerInventory playerInventory;

    private float outputDelay;
    private float outputDelayDelta;
    void Start()
    {
        outputDelay = cookingSpot.outputDelay;
    }
    public override void OnPlayerEnter()
    {
        if (!playerInventory.HasSpace())
        {
            UIWarningPopup.Instance.ShowWarning("You have no space left in inventory!", 4f);
        }
    }
    public override void OnPlayerStay()
    {
        outputDelayDelta -= Time.deltaTime;

        if (outputDelayDelta <= 0 && playerInventory.HasSpace())
        {
            FishItem fish = cookingSpot.OutputFish();

            if (fish != null)
            {
                outputDelayDelta = outputDelay;

                playerInventory.AddFishItem(fish, true);
            }
        }
    }
}
