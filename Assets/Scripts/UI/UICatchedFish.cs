using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using VContainer;

public class UICatchedFish : MonoBehaviour
{
    [SerializeField] private RawImage fishIcon;
    [SerializeField] private TextMeshProUGUI labelTxt;
    [SerializeField] private TextMeshProUGUI sizeTxt;
    [SerializeField] private TextMeshProUGUI valueTxt;
    [Inject] private UICatchedFishIcon uICatchedFishIcon;

    RectTransform rt;
    private float panelWidth;
    private float panelOffset;
    Sequence sequence;
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        panelWidth = rt.rect.width;

        panelOffset = panelWidth / 20;

        rt.anchoredPosition = new Vector2(panelWidth + panelOffset, 0);
    }

    public void OpenUI(Fish fish)
    {
        labelTxt.SetText(fish.nameString);
        sizeTxt.SetText($"Size: {fish.size.ToString("F2")}m");
        valueTxt.SetText($"Value: {fish.sellValue}");

        uICatchedFishIcon.SetIcon(fish, 5f);

        PlayAnimation();
    }
    private void PlayAnimation()
    {
        if (sequence != null && sequence.IsActive() && sequence.IsPlaying()) sequence.Kill();

        sequence = DOTween.Sequence();

        rt.DOPunchScale(Vector2.one * 0.2f, 0.4f, 0, 0.2f);

        sequence.Append(rt.DOAnchorPosX(0, 0.3f).SetEase(Ease.OutQuad));
        sequence.AppendInterval(4f);
        sequence.Append(rt.DOAnchorPosX(panelWidth + panelOffset, 0.3f).SetEase(Ease.InQuad));
    }
}
