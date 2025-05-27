using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CookingSpotPickupZone : EventZone
{
    private CookingSpot cookingSpot;
    [Inject] private PlayerInventory playerInventory;
    private float outputDelay;
    private float outputDelayDelta = 0;

    [Inject]
    public void Construct(CookingSpot cookingSpot)
    {
        this.cookingSpot = cookingSpot;

        outputDelay = cookingSpot.cooldownToPickupFish;
    }

    public override void OnPlayerEnter()
    {
        outputDelayDelta = 0;
    }
    public override void OnPlayerStay()
    {
        outputDelayDelta -= Time.deltaTime;

        if (playerInventory.ItemsAmount() != 0 && outputDelayDelta <= 0)
        {
            FishItem fish = playerInventory.TakeFirstRawFish();

            if (fish != null)
            {
                cookingSpot.PickUpFish(fish);
            }

            outputDelayDelta = outputDelay;
        }
    }
}
