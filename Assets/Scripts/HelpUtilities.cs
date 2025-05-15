using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelpUtilities
{
    public static Fish ToFish(this FishSO fishSO, float _size, int _sellValue)
    {
        Fish fish = new Fish
        {
            nameString = fishSO.nameString,
            strength = fishSO.strength,
            speed = fishSO.speed,
            rarity = fishSO.rarity,
            size = _size,
            sellValue = _sellValue,
            fishModel = fishSO.fishModel,
            width = fishSO.width
        };

        return fish;
    }
}
