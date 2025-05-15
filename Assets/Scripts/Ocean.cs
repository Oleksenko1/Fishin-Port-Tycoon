using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    [SerializeField] private List<FishSO> fishList;

    public FishSO GetFish()
    {
        return fishList[Random.Range(0, fishList.Count)];
    }
}
