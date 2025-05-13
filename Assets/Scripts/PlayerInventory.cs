using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [Header("Components")]
    [SerializeField] private Transform inventoryTransform;

    public List<FishSO> startItems = new List<FishSO>();
    private List<FishItem> items = new List<FishItem>();

    void Awake()
    {
        // Initialize inventory
        if (startItems.Count > 0)
        {
            for (int i = 0; i < startItems.Count; i++)
            {
                AddFish(startItems[i]);
            }
        }
    }
    public void AddFish(FishSO fishSO)
    {
        var fish = Instantiate(fishSO.fishModel, inventoryTransform);
        var fishItem = fish.AddComponent<FishItem>();
        fishItem.fishSO = fishSO;

        // Configuring Y position
        if (items.Count > 0)
        {
            var lastFish = items[items.Count - 1];
            Vector3 lastFishPos = lastFish.transform.localPosition;

            float heightOffset = lastFish.fishSO.width / 2 + fishItem.fishSO.width / 2;
            Vector3 newFishPos = lastFishPos + Vector3.up * heightOffset;

            fish.transform.localPosition = newFishPos;
        }
        else
        {
            fish.transform.localPosition = Vector3.up * fishItem.fishSO.width / 2;
        }

        fish.transform.Rotate(Vector3.up * -90);
        fish.transform.Rotate(Vector3.forward * 90);

        items.Add(fishItem);
    }
    public FishItem TakeFish()
    {
        return null;
    }
}
