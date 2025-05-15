using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    [SerializeField] private List<FishSO> fishList;

    public Fish GetFish()
    {
        FishSO fishSO = fishList[Random.Range(0, fishList.Count)];

        float size = Random.Range(fishSO.minSize, fishSO.maxSize);

        float sizeValue = size / ((fishSO.minSize + fishSO.maxSize) / 2);
        int sellValue = (int)(fishSO.sellValue * sizeValue);

        Fish fish = fishSO.ToFish(size, sellValue);

        return fish;
    }
}
