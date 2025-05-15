using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelpUtilities 
{
    public static Fish ToFish(this FishSO fishSO, float size, int sellValue)
    {
        Fish fish = new Fish
        {
            name = fishSO.name,
            rarity = fishSO.rarity,
            size = size,
            sellValue = sellValue
        };
        
        return fish;
    }
}
