using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CookingSpotPickupZone : EventZone
{
    private CookingSpot cookingSpot;
    [Inject] private PlayerInventory playerInventory;

    [Inject]
    public void Construct(CookingSpot cookingSpot)
    {
        this.cookingSpot = cookingSpot;
        cookingSpot.SetPickupZone(this);
    }
    private bool canPickup = true;

    public override void OnPlayerEnter()
    {
        canPickup = true;
    }
    public override void OnPlayerStay()
    {
        if (canPickup == false) return;

        if (cookingSpot.IsFishGettingPicked() == false)
        {
            // Do something
            FishItem fish = playerInventory.TakeFirstRawFish();

            if (fish == null)
            {
                canPickup = false;
            }
            else
            {
                cookingSpot.PickUpFish(fish);
            }
        }
    }
    public override void OnPlayerExit()
    {
        canPickup = false;
    }
    public bool IsInUse() => canPickup;
}
