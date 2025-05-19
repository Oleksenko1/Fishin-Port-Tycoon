using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIWarningPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningTxt;
    [SerializeField] private Color textColor;
    public static UIWarningPopup Instance;

    private Color clearColor;
    Sequence sequence;
    void Awake()
    {
        Instance = this;

        clearColor = textColor;
        clearColor.a = 0;

        HideUI();
    }
    public void ShowWarning(string message, float showTime)
    {
        gameObject.SetActive(true);

        warningTxt.SetText(message);

        if (sequence != null && sequence.IsActive() && sequence.IsPlaying()) sequence.Kill();

        sequence = DOTween.Sequence();

        // Animation

        warningTxt.color = clearColor;
        warningTxt.rectTransform.localScale = Vector3.one;

        sequence.Append(warningTxt.DOColor(textColor, 0.2f).SetEase(Ease.InQuad));
        sequence.Join(warningTxt.rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.4f).SetEase(Ease.InQuad));

        sequence.AppendInterval(showTime);

        sequence.Append(warningTxt.DOColor(clearColor, 0.5f).SetEase(Ease.OutQuad));
        sequence.Join(warningTxt.rectTransform.DOScale(Vector3.one * 0.5f, 0.6f).SetEase(Ease.InQuad));

        sequence.OnComplete(HideUI);
    }
    private void HideUI()
    {
        gameObject.SetActive(false);
    }
}
