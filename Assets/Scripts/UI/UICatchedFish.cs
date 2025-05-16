using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICatchedFish : MonoBehaviour
{
    [SerializeField] private RawImage fishIcon;
    [SerializeField] private TextMeshProUGUI labelTxt;
    [SerializeField] private TextMeshProUGUI sizeTxt;
    [SerializeField] private TextMeshProUGUI valueTxt;

    RectTransform rt;
    private float panelWidth;
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        panelWidth = rt.rect.width;

        rt.anchoredPosition = new Vector2(panelWidth, 0);
    }

    public void OpenUI(Fish fish)
    {
        
    }
    private void HideUI()
    {

    }
}
