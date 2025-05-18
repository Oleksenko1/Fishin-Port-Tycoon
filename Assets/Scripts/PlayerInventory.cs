using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [Header("Components")]
    [SerializeField] private Transform inventoryTransform;

    private List<FishItem> items = new List<FishItem>();

    public void AddFish(Fish fish)
    {
        var fishObject = Instantiate(fish.fishModel, inventoryTransform);
        fishObject.GetComponentInChildren<Renderer>().material = fish.fishMaterial;
        var fishItem = fishObject.AddComponent<FishItem>();
        fishItem.fish = fish;

        // Configuring Y position
        if (items.Count > 0)
        {
            var lastFish = items[items.Count - 1];
            Vector3 lastFishPos = lastFish.transform.localPosition;

            float heightOffset = lastFish.fish.width / 2 + fishItem.fish.width / 2;
            Vector3 newFishPos = lastFishPos + Vector3.up * heightOffset;

            fishObject.transform.localPosition = newFishPos;
        }
        else
        {
            fishObject.transform.localPosition = Vector3.up * fishItem.fish.width / 2;
        }

        fishObject.transform.Rotate(Vector3.up * -90);
        fishObject.transform.Rotate(Vector3.forward * 90);

        items.Add(fishItem);
    }
    public FishItem TakeFish()
    {
        if (items.Count == 0) return null;

        int lastIndex = items.Count - 1;

        FishItem fish = items[lastIndex];
        Destroy(items[lastIndex].gameObject);
        items.RemoveAt(lastIndex);

        return fish;
    }

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
