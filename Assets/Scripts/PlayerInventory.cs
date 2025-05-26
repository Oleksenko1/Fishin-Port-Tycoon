using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [Header("Components")]
    [SerializeField] private Transform inventoryTransform;
    [Inject] private FishObjectPool objectPool;

    private List<FishItem> items = new List<FishItem>();

    public void AddNewFish(Fish fish)
    {
        var fishItem = objectPool.GetFish(fish);

        fishItem.fish = fish;
        fishItem.renderer.material = fish.fishMaterial;

        SetFishStrictPosition(fishItem);

        items.Add(fishItem);
    }
    public void AddFishItem(FishItem fishItem, bool animateMovement)
    {
        var fishTransform = fishItem.transform;

        DOTween.Kill(fishTransform);

        if (animateMovement) // Moves fish to inventory with animation
        {
            fishTransform.SetParent(inventoryTransform);

            Vector3 targetPosition = ConfigureTargetPosition(fishItem);

            fishTransform.DOLocalMove(targetPosition, 0.25f).SetEase(Ease.InQuad);
            fishTransform.DOLocalRotate(new Vector3(0, -90, 90), 0.2f, RotateMode.FastBeyond360);
        }
        else
        {
            SetFishStrictPosition(fishItem);
        }

        items.Add(fishItem);
    }
    public FishItem TakeAndRemoveFish()
    {
        if (items.Count == 0) return null;

        int lastIndex = items.Count - 1;

        FishItem fish = items[lastIndex];
        fish.transform.SetParent(null);

        objectPool.ReturnFish(fish);
        items.RemoveAt(lastIndex);

        return fish;
    }
    public FishItem TakeFirstRawFish()
    {
        if (items.Count == 0) return null;

        for (int i = items.Count - 1; i >= 0; i--)
        {
            FishItem fish = items[i];

            if (!fish.fish.isCooked)
            {
                fish.transform.SetParent(null);

                items.RemoveAt(i);

                return fish;
            }
        }

        return null;
    }
    public bool HasSpace() => items.Count < maxCapacity;

    // Calculate position where fish should be placed
    private Vector3 ConfigureTargetPosition(FishItem fishItem)
    {
        Vector3 targetPosition;

        // Configuring Y position
        if (items.Count > 0)
        {
            var lastFish = items[items.Count - 1];
            Vector3 lastFishPos = lastFish.transform.localPosition;

            float heightOffset = lastFish.fish.width / 2 + fishItem.fish.width / 2;
            Vector3 newFishPos = lastFishPos + Vector3.up * heightOffset;

            targetPosition = newFishPos;
        }
        else
        {
            targetPosition = Vector3.up * fishItem.fish.width / 2;
        }

        return targetPosition;
    }
    // Set strict position of fish in inventory if it does not use animations
    private void SetFishStrictPosition(FishItem fishItem)
    {
        var fishTransform = fishItem.transform;

        fishTransform.SetParent(inventoryTransform);
        fishTransform.localPosition = Vector3.zero;
        fishTransform.localRotation = Quaternion.identity;

        Vector3 targetPosition = ConfigureTargetPosition(fishItem);

        fishTransform.localPosition = targetPosition;

        fishTransform.Rotate(Vector3.up * -90);
        fishTransform.Rotate(Vector3.forward * 90);
    }

#if UNITY_EDITOR
    // Code for testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeAndRemoveFish();
        }
    }
#endif
}
