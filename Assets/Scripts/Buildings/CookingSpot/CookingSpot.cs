using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using DG.Tweening;

public class CookingSpot : MonoBehaviour
{
    [Tooltip("Cooldown between each fish taken from player inventory")]
    [SerializeField] public float cooldownToPickupFish;
    [Header("Components")]
    [SerializeField] private Transform inputFishStackPos;
    [Inject] private FishObjectPool fishObjectPool;
    private Stack<FishItem> fishToCook = new Stack<FishItem>();

    public void PickUpFish(FishItem fishItem)
    {
        PlayPickupAnimation(fishItem);

        fishToCook.Push(fishItem);
    }
    private void PlayPickupAnimation(FishItem fishItem)
    {
        Vector3 targetLocalPos = CalculateTargetLocalPosition(fishItem);

        Vector3 worldStartPos = fishItem.transform.position;
        Vector3 worldTargetPos = inputFishStackPos.TransformPoint(targetLocalPos);

        fishItem.transform.SetParent(null);
        fishItem.transform.position = worldStartPos;

        float peakHeight = 3f;
        float duration = 1f;

        Vector3 peakYPos = new Vector3(worldStartPos.x, worldStartPos.y + peakHeight, worldStartPos.z);
        Vector3 midXZ = new Vector3(
            (worldStartPos.x + worldTargetPos.x) / 2f,
            peakYPos.y,
            (worldStartPos.z + worldTargetPos.z) / 2f
        );

        Vector3 finalYPos = new Vector3(worldTargetPos.x, worldTargetPos.y + peakHeight, worldTargetPos.z);

        Sequence seq = DOTween.Sequence();

        Quaternion targetRotation = Quaternion.Euler(0f, -90f, -90f);

        seq.Append(fishItem.transform.DOMoveY(peakYPos.y, duration / 2f).SetEase(Ease.OutQuad));

        seq.Join(fishItem.transform.DOMoveX(midXZ.x, duration / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOMoveZ(midXZ.z, duration / 2f).SetEase(Ease.Linear));

        seq.Join(fishItem.transform.DORotate(targetRotation.eulerAngles, duration / 2f).SetEase(Ease.InOutSine));

        seq.Append(fishItem.transform.DOMoveY(worldTargetPos.y, duration / 2f).SetEase(Ease.InQuad));
        seq.Join(fishItem.transform.DOMoveX(worldTargetPos.x, duration / 2f).SetEase(Ease.Linear));
        seq.Join(fishItem.transform.DOMoveZ(worldTargetPos.z, duration / 2f).SetEase(Ease.Linear));

        seq.OnComplete(() =>
        {
            fishItem.transform.SetParent(inputFishStackPos);
            fishItem.transform.localPosition = targetLocalPos;
        });

    }

    private Vector3 CalculateTargetLocalPosition(FishItem newFish)
    {
        float totalHeight = 0f;

        foreach (var fish in fishToCook)
        {
            totalHeight += fish.fish.width;
        }

        totalHeight += newFish.fish.width / 2f;

        return Vector3.up * totalHeight;
    }
}
