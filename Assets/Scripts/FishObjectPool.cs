using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.EditorTools;
using UnityEngine;

public class FishObjectPool : MonoBehaviour
{
    [SerializeField] private List<FishSO> startingPool;
    [Tooltip("Starting amount of each element")]
    [SerializeField] private int startingAmount;

    private Dictionary<string, Queue<FishItem>> fishPool = new Dictionary<string, Queue<FishItem>>();

    void Awake()
    {
        InitializePool();
    }
    private void InitializePool()
    {
        foreach (FishSO fishSO in startingPool)
        {
            for (int i = 0; i < startingAmount; i++)
            {
                var fishObject = Instantiate(fishSO.fishModel);
                var fishItemScript = fishObject.GetComponent<FishItem>();

                // Adds new queue of fish type if it doesn't exist yet
                if (fishPool.ContainsKey(fishSO.nameString) == false)
                {
                    fishPool.Add(fishSO.nameString, new Queue<FishItem>());
                }

                fishPool[fishSO.nameString].Enqueue(fishItemScript);

                Debug.Log($"Fish {fishSO.nameString} was added");

                fishObject.gameObject.SetActive(false);
            }
        }
    }
    public FishItem GetFish(string fishName)
    {
        if (fishPool.ContainsKey(fishName) == false) return null;

        Queue<FishItem> pool = fishPool[fishName];

        if (pool.Count > 0)
        {
            FishItem fish = pool.Dequeue();
            fish.gameObject.SetActive(true);
            return fish;
        }
        else
        {
            FishItem fish = Instantiate(FindFishSO_ByName(fishName).fishModel).GetComponent<FishItem>();
            return fish;
        }
    }
    public void ReturnFish(FishItem fish)
    {
        fish.gameObject.SetActive(false);

        string fishName = fish.fish.nameString;

        if (fishPool.ContainsKey(fishName) == false) return;

        fishPool[fishName].Enqueue(fish);
    }
    private FishSO FindFishSO_ByName(string fishName)
    {
        FishSO fishSO = null;
        foreach (FishSO fish in startingPool)
        {
            if (fish.nameString.Equals(fishName))
            {
                fishSO = fish;
                break;
            }
        }

        if (fishSO == null)
            Debug.LogError("No such fish in starting pool");

        return fishSO;
    }
}
