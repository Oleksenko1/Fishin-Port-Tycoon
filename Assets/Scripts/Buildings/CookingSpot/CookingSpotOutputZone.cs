using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CookingSpotOutputZone : EventZone
{
    [Inject] private CookingSpot cookingSpot;
    [Inject] private PlayerInventory playerInventory;
}
