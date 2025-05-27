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
    [Tooltip("Delay between fish being taken from cooking spot to a player")]
    [SerializeField] public float outputDelay;
    [Header("Components")]
    [SerializeField] private Transform inputFishStackPos;
    [SerializeField] private Transform cookingPos;
    [SerializeField] private Transform outputFishStackPos;
    [Space(15)]
    [SerializeField] private Material cookedMaterial;
    private Stack<FishItem> fishToCook = new Stack<FishItem>();
    private Stack<FishItem> fishReady = new Stack<FishItem>();

    private FishItem currentCookingFish;
    private float cookDelayDelta = 0;

    void Update()
    {
        cookDelayDelta -= Time.deltaTime;

        if (fishToCook.Count == 0 && currentCookingFish == null) return;

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
    public FishItem OutputFish()
    {
        FishItem fish = null;

        if (fishReady.Count != 0)
        {
            fish = fishReady.Pop();
        }

        return fish;
    }
    private void CookCurrentFish()
    {
        PlayCurrentFishToCookingSpotAnimation();
    }
    private void FinishCookingCurrentFish()
    {
        currentCookingFish.fish.isCooked = true;
        currentCookingFish.renderer.material = cookedMaterial;

        PlayOutputAnimation(currentCookingFish);

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
        Vector3 targetLocalPos = CalculateTargetLocalPosition(fishItem, fishToCook);

        Vector3 startPos = fishItem.transform.localPosition;

        fishItem.transform.SetParent(inputFishStackPos);
        fishItem.transform.position = startPos;

        float peakHeight = 3f;

        Vector3 peakYPos = new Vector3(startPos.x, startPos.y + peakHeight, startPos.z);
        Vector3 midXZ = new Vector3(
            (startPos.x + targetLocalPos.x) / 2f,
            peakYPos.y,
            (startPos.z + targetLocalPos.z) / 2f
        );

        Sequence seq = DOTween.Sequence();

        float animationLength = 0.4f;

        Quaternion targetRotation = Quaternion.Euler(0f, -90f, -90f);

        seq.Append(fishItem.transform.DOLocalMoveY(peakYPos.y, animationLength / 2f).SetEase(Ease.OutQuad));

        seq.Join(fishItem.transform.DOLocalMoveX(midXZ.x, animationLength / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOLocalMoveZ(midXZ.z, animationLength / 2f).SetEase(Ease.Linear));

        seq.Join(fishItem.transform.DORotate(targetRotation.eulerAngles, animationLength / 2f).SetEase(Ease.InOutSine));

        seq.Append(fishItem.transform.DOLocalMoveY(targetLocalPos.y, animationLength / 2f).SetEase(Ease.InQuad));
        seq.Join(fishItem.transform.DOLocalMoveX(targetLocalPos.x, animationLength / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOLocalMoveZ(targetLocalPos.z, animationLength / 2f).SetEase(Ease.Linear));

        fishToCook.Push(fishItem);
    }

    private void PlayOutputAnimation(FishItem fishItem)
    {
        // DO: Upgrade animation

        fishItem.transform.SetParent(null);

        Vector3 localTargetPosition = CalculateTargetLocalPosition(fishItem, fishReady);

        Vector3 worldTargetPos = outputFishStackPos.TransformPoint(localTargetPosition);

        float animationDelay = 0.5f;

        fishItem.transform.DOMove(worldTargetPos, animationDelay).SetEase(Ease.OutQuad);
    }

    private void PlayCurrentFishToCookingSpotAnimation()
    {
        // DO: Upgrade animation
        Sequence sequence = DOTween.Sequence();

        DOTween.Kill(currentCookingFish.transform);

        currentCookingFish.transform.SetParent(cookingPos, worldPositionStays: true);

        Vector3 targetPosition = Vector3.up * currentCookingFish.fish.width / 2;

        float animationDelay = 0.5f;

        sequence.Append(currentCookingFish.transform.DOLocalMove(targetPosition, animationDelay).SetEase(Ease.OutQuad));
        sequence.Join(currentCookingFish.transform.DORotate(new Vector3(0f, -90f, -90f), animationDelay).SetEase(Ease.OutQuad));
    }

}
