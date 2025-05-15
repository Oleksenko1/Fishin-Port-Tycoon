using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIFishingBar : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float basicArrowSpeed;
    [Header("Components")]
    [SerializeField] private RectTransform greenZone;
    [SerializeField] private RectTransform catchArrow;

    private Tween arrowTween;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    public void PrepareFishing(FishStrength fishStrength, FishSpeed fishSpeed)
    {
        float zoneWidth = GetZoneWidth(fishStrength);
        SetGreenZone(zoneWidth);

        float arrowSpeed = GetArrowSpeedMultiplier(fishSpeed);
        StartArrowMovement(arrowSpeed);
    }

    private void SetGreenZone(float zoneWidth)
    {
        float maxSpawnPos = 1 - zoneWidth;

        float minX = Random.Range(0, maxSpawnPos);
        float maxX = minX + zoneWidth;

        greenZone.anchorMin = new Vector2(minX, 0f);
        greenZone.anchorMax = new Vector2(maxX, 1f);
        greenZone.offsetMin = Vector2.zero;
        greenZone.offsetMax = Vector2.zero;
    }

    private void StartArrowMovement(float speedMultiplier)
    {
        catchArrow.anchoredPosition = new Vector2(0f, catchArrow.anchoredPosition.y);

        float halfWidth = ((RectTransform)catchArrow.parent).rect.width;

        float duration = basicArrowSpeed / speedMultiplier;

        arrowTween?.Kill();

        arrowTween = catchArrow.DOAnchorPosX(halfWidth, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
    public bool CatchFish()
    {
        arrowTween?.Kill();

        RectTransform parentRect = (RectTransform)catchArrow.parent;
        float parentWidth = parentRect.rect.width;

        float greenMinX = greenZone.anchorMin.x * parentWidth;
        float greenMaxX = greenZone.anchorMax.x * parentWidth;

        float arrowX = catchArrow.anchoredPosition.x;

        bool isFishCatched = arrowX >= greenMinX && arrowX <= greenMaxX;

        return isFishCatched;
    }

    public void ShowUI() => gameObject.SetActive(true);
    public void HideUI() => gameObject.SetActive(false);

    private float GetZoneWidth(FishStrength strength) => strength switch
    {
        FishStrength.VeryWeak => 0.3f,
        FishStrength.Weak => 0.27f,
        FishStrength.Average => 0.23f,
        FishStrength.Strong => 0.2f,
        FishStrength.VeryStrong => 0.17f,
        FishStrength.Overpowered => 0.12f,
        FishStrength.Godlike => 0.8f,
        _ => 0.3f
    };

    private float GetArrowSpeedMultiplier(FishSpeed speed) => speed switch
    {
        FishSpeed.VerySlow => 1f,
        FishSpeed.Slow => 1.2f,
        FishSpeed.Average => 1.4f,
        FishSpeed.Fast => 1.6f,
        FishSpeed.VeryFast => 1.8f,
        FishSpeed.Flash => 2.1f,
        FishSpeed.LightSpeed => 2.4f,
        _ => 1f
    };

}

