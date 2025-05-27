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

        RepositionInventoryFish(lastIndex);

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

                RepositionInventoryFish(i);

                return fish;
            }
        }

        return null;
    }
    public bool HasSpace() => items.Count < maxCapacity;

    // Calculate position where fish should be placed
    private Vector3 ConfigureTargetPosition(FishItem fishItem)
    {
        float currentY = 0f;

        // Calculating previous fish height
        for (int i = 0; i < items.Count; i++)
        {
            currentY += items[i].fish.width;
        }

        currentY += fishItem.fish.width / 2f;

        return Vector3.up * currentY;
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
    private void RepositionInventoryFish(int index)
    {
        if (index >= items.Count) return;

        float currentY = 0f;

        // Calculating starting Y position
        for (int i = 0; i < index; i++)
        {
            float heightOffset = items[i].fish.width;
            currentY += heightOffset;
        }

        // Reposition all fish, starting from INDEX
        for (int i = index; i < items.Count; i++)
        {
            var fishItem = items[i];
            var fishTransform = fishItem.transform;

            float halfHeight = fishItem.fish.width / 2f;
            currentY += halfHeight;

            Vector3 newPosition = Vector3.up * currentY;

            DOTween.Kill(fishTransform);
            fishTransform.DOLocalMove(newPosition, 0.2f).SetEase(Ease.OutQuad);

            currentY += halfHeight;
        }
    }
    public int ItemsAmount() => items.Count;


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
