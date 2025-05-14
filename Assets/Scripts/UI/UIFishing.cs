using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFishing : MonoBehaviour
{
    public event Action OnCatchPressed;
    public event Action OnExitPressed;
    [SerializeField] private Button catchButton;
    [SerializeField] private Button exitBtn;

    void Awake()
    {
        catchButton.onClick.AddListener(() =>
        {
            OnCatchPressed?.Invoke();
        });

        exitBtn.onClick.AddListener(() =>
        {
            OnExitPressed?.Invoke();
        });

        HideUI();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
}
