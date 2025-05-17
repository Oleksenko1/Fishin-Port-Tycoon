using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class FishingController : MonoBehaviour
{
    public event Action OnExit;
    [Tooltip("Minimum time to find a fish")]
    [SerializeField] private float minTimeToCatch = 3f;
    [Tooltip("Maximum time to find a fish")]
    [SerializeField] private float maxTimeToCatch = 5f;
    [Inject] private UIFishing fishingUI;
    [Inject] private UIFishingBar fishingBarUI;
    [Inject] private UICatchedFish catchedFishUI;
    [Inject] private PlayerInventory playerInventory;
    [Inject] private Ocean ocean;
    private Fish currentFish;
    private float timeToCatch;
    private bool isFishing = false;

    void Start()
    {
        fishingUI.OnCatchPressed += CatchFish;
        fishingUI.OnExitPressed += ExitFishing;
    }
    public void StartFishing()
    {
        timeToCatch = UnityEngine.Random.Range(minTimeToCatch, maxTimeToCatch);

        fishingUI.ShowUI();

        StartCoroutine(nameof(GetFishOnHook));
    }
    private IEnumerator GetFishOnHook()
    {
        currentFish = ocean.GetFish();

        yield return new WaitForSeconds(timeToCatch);

        fishingBarUI.PrepareFishing(currentFish);
        fishingBarUI.ShowUI();
        isFishing = true;
    }

    private void CatchFish()
    {
        if (!isFishing) return;

        bool isSuccess = fishingBarUI.CatchFish();

        if (isSuccess)
        {
            playerInventory.AddFish(currentFish);

            catchedFishUI.OpenUI(currentFish);
        }

        isFishing = false;

        fishingBarUI.HideUI();

        StartCoroutine(nameof(GetFishOnHook));
    }
    private void ExitFishing()
    {
        if (isFishing) return;

        fishingUI.HideUI();

        StopCoroutine(nameof(GetFishOnHook));

        OnExit?.Invoke();
    }
}
