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
    public override void OnPlayerExit()
    {
        canPickup = false;
    }
    public bool IsInUse() => canPickup;
}
