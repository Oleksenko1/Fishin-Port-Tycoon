using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using DG.Tweening;
using UnityEngine.UIElements;

public class CookingSpot : MonoBehaviour
{
    [Tooltip("Cooldown between each fish taken from player inventory")]
    [SerializeField] public float cooldownToPickupFish;
    [Tooltip("Time delay to cook one fish")]
    [SerializeField] private float cookDelay;
    [Header("Components")]
    [SerializeField] private Transform inputFishStackPos;
    [SerializeField] private Transform cookingPos;
    [SerializeField] private Transform outputFishStackPos;
    // [Inject] private FishObjectPool fishObjectPool;
    private CookingSpotPickupZone cookingSpotPickupZone;
    private Stack<FishItem> fishToCook = new Stack<FishItem>();
    private Stack<FishItem> fishReady = new Stack<FishItem>();

    private FishItem currentCookingFish;
    private float cookDelayDelta = 0;
    private bool isFishGettingPicked = false;

    void Update()
    {
        if ((fishToCook.Count == 0 && currentCookingFish == null) || cookingSpotPickupZone.IsInUse()) return;

        cookDelayDelta -= Time.deltaTime;

        if (cookDelayDelta <= 0)
        {
            // Finishing cooking last fish if it is not null
            if (currentCookingFish != null)
            {
                FinishCookingCurrentFish();
            }

            // Take new fish to cook if there are any left
            if (fishToCook.Count > 0)
            {
                currentCookingFish = fishToCook.Pop();

                CookCurrentFish();
            }

            cookDelayDelta = cookDelay;
        }
    }
    public void PickUpFish(FishItem fishItem)
    {
        PlayPickupAnimation(fishItem);
    }
    private void CookCurrentFish()
    {
        PlayCurrentFishToCookingSpotAnimation();
    }
    private void FinishCookingCurrentFish()
    {
        currentCookingFish.fish.isCooked = true;

        fishReady.Push(currentCookingFish);

        currentCookingFish = null;
    }
    private Vector3 CalculateTargetLocalPosition(FishItem newFish, Stack<FishItem> fishStack)
    {
        float totalHeight = 0f;

        foreach (var fish in fishStack)
        {
            totalHeight += fish.fish.width;
        }

        totalHeight += newFish.fish.width / 2f;

        return Vector3.up * totalHeight;
    }
    private void PlayPickupAnimation(FishItem fishItem)
    {
        isFishGettingPicked = true;

        Vector3 targetLocalPos = CalculateTargetLocalPosition(fishItem, fishToCook);

        Vector3 worldStartPos = fishItem.transform.position;
        Vector3 worldTargetPos = inputFishStackPos.TransformPoint(targetLocalPos);

        fishItem.transform.SetParent(null);
        fishItem.transform.position = worldStartPos;

        float peakHeight = 3f;

        Vector3 peakYPos = new Vector3(worldStartPos.x, worldStartPos.y + peakHeight, worldStartPos.z);
        Vector3 midXZ = new Vector3(
            (worldStartPos.x + worldTargetPos.x) / 2f,
            peakYPos.y,
            (worldStartPos.z + worldTargetPos.z) / 2f
        );

        Vector3 finalYPos = new Vector3(worldTargetPos.x, worldTargetPos.y + peakHeight, worldTargetPos.z);

        Sequence seq = DOTween.Sequence();

        Quaternion targetRotation = Quaternion.Euler(0f, -90f, -90f);

        seq.Append(fishItem.transform.DOMoveY(peakYPos.y, cooldownToPickupFish / 2f).SetEase(Ease.OutQuad));

        seq.Join(fishItem.transform.DOMoveX(midXZ.x, cooldownToPickupFish / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOMoveZ(midXZ.z, cooldownToPickupFish / 2f).SetEase(Ease.Linear));

        seq.Join(fishItem.transform.DORotate(targetRotation.eulerAngles, cooldownToPickupFish / 2f).SetEase(Ease.InOutSine));

        seq.Append(fishItem.transform.DOMoveY(worldTargetPos.y, cooldownToPickupFish / 2f).SetEase(Ease.InQuad));
        seq.Join(fishItem.transform.DOMoveX(worldTargetPos.x, cooldownToPickupFish / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOMoveZ(worldTargetPos.z, cooldownToPickupFish / 2f).SetEase(Ease.Linear));

        seq.OnComplete(() =>
        {
            fishItem.transform.SetParent(inputFishStackPos);
            fishItem.transform.localPosition = targetLocalPos;


            isFishGettingPicked = false;

            fishToCook.Push(fishItem);
        });

    }

    private void PlayCurrentFishToCookingSpotAnimation()
    {
        // DO: Upgrade animation
        Sequence sequence = DOTween.Sequence();

        currentCookingFish.transform.SetParent(null);

        float animationDelay = 0.5f;

        sequence.Append(currentCookingFish.transform.DOMove(cookingPos.position, animationDelay).SetEase(Ease.OutQuad));

        sequence.OnComplete(() =>
        {
            currentCookingFish.transform.SetParent(cookingPos);
        });
    }
    public bool IsFishGettingPicked() => isFishGettingPicked;

    public void SetPickupZone(CookingSpotPickupZone pickupZone)
    {
        cookingSpotPickupZone = pickupZone;
    }
}
