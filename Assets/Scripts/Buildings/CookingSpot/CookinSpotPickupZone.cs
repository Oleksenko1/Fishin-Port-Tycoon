using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CookinSpotPickupZone : EventZone
{
    [Inject] private CookingSpot cookingSpot;
    [Inject] private PlayerInventory playerInventory;

    private float pickUpCooldown;
    private float pickUpCooldownDelta;
    private bool canPickup = true;
    private void Start()
    {
        pickUpCooldown = cookingSpot.cooldownToPickupFish;

        pickUpCooldownDelta = 0;
    }
    public override void OnPlayerEnter()
    {
        pickUpCooldownDelta = 0;

        canPickup = true;
    }
    public override void OnPlayerStay()
    {
        if (canPickup == false) return;

        pickUpCooldownDelta -= Time.deltaTime;

        if (pickUpCooldownDelta <= 0)
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

            pickUpCooldownDelta = pickUpCooldown;
        }
    }
}
