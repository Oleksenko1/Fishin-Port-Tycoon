using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [Header("Components")]
    [SerializeField] private Transform inventoryTransform;
    [Inject] private FishObjectPool objectPool;

    private List<FishItem> items = new List<FishItem>();

    public void AddFish(Fish fish)
    {
        var fishItem = objectPool.GetFish(fish);

        var fishTransform = fishItem.transform;
        fishItem.fish = fish;
        fishItem.renderer.material = fish.fishMaterial;

        fishTransform.SetParent(inventoryTransform);
        fishTransform.localPosition = Vector3.zero;
        fishTransform.localRotation = Quaternion.identity;

        // Configuring Y position
        if (items.Count > 0)
        {
            var lastFish = items[items.Count - 1];
            Vector3 lastFishPos = lastFish.transform.localPosition;

            float heightOffset = lastFish.fish.width / 2 + fishItem.fish.width / 2;
            Vector3 newFishPos = lastFishPos + Vector3.up * heightOffset;

            fishTransform.localPosition = newFishPos;
        }
        else
        {
            fishTransform.localPosition = Vector3.up * fishItem.fish.width / 2;
        }

        fishTransform.Rotate(Vector3.up * -90);
        fishTransform.Rotate(Vector3.forward * 90);

        items.Add(fishItem);
    }
    public FishItem TakeFish()
    {
        if (items.Count == 0) return null;

        int lastIndex = items.Count - 1;

        FishItem fish = items[lastIndex];
        fish.transform.SetParent(null);

        objectPool.ReturnFish(fish);
        items.RemoveAt(lastIndex);

        return fish;
    }
    public bool HasSpace() => items.Count < maxCapacity;

#if UNITY_EDITOR
    // Code for testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeFish();
        }
    }
#endif
}
