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

    // Fish ObjectPool that is accessed by fish model
    private Dictionary<Transform, Queue<FishItem>> fishPool = new Dictionary<Transform, Queue<FishItem>>();

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
                fishItemScript.fish = fishSO.ToFish(0, 0);

                // Adds new queue of fish type if it doesn't exist yet
                if (fishPool.ContainsKey(fishSO.fishModel) == false)
                {
                    fishPool.Add(fishSO.fishModel, new Queue<FishItem>());
                }

                fishPool[fishSO.fishModel].Enqueue(fishItemScript);

                fishObject.gameObject.SetActive(false);
            }
        }
    }
    public FishItem GetFish(Fish fishItem)
    {
        if (fishPool.ContainsKey(fishItem.fishModel) == false) return null;

        Queue<FishItem> pool = fishPool[fishItem.fishModel];

        if (pool.Count > 0)
        {
            FishItem fish = pool.Dequeue();
            fish.gameObject.SetActive(true);
            return fish;
        }
        else
        {
            FishItem fish = Instantiate(FindFishSO(fishItem).fishModel).GetComponent<FishItem>();
            fish.fish = fishItem;
            return fish;
        }
    }

    public void ReturnFish(FishItem fish)
    {
        fish.gameObject.SetActive(false);

        Transform fishModel = fish.fish.fishModel;

        if (fishPool.ContainsKey(fishModel) == false)
        {
            Debug.LogError("No such fish model in fish object pool");
            return;
        }

        fish.transform.position = Vector3.zero;
        fishPool[fishModel].Enqueue(fish);
    }
    private FishSO FindFishSO(Fish fishItem)
    {
        FishSO fishSO = null;
        foreach (FishSO fish in startingPool)
        {
            if (fish.fishModel == fishItem.fishModel)
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
