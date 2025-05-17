using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    public event Action OnExit;
    private UIFishing fishingUI;
    private UIFishingBar fishingBarUI;
    private UICatchedFish catchedFishUI;
    private PlayerInventory playerInventory;
    private Ocean ocean;

    [Tooltip("Minimum time to find a fish")]
    [SerializeField] private float minTimeToCatch = 3f;
    [Tooltip("Maximum time to find a fish")]
    [SerializeField] private float maxTimeToCatch = 5f;
    private Fish currentFish;
    private float timeToCatch;
    private bool isFishing = false;

    [VContainer.Inject]
    public void Construct(UIFishing fishingUI, UIFishingBar fishingBarUI, UICatchedFish catchedFishUI, PlayerInventory playerInventory, Ocean ocean)
    {
        this.fishingUI = fishingUI;
        this.fishingBarUI = fishingBarUI;
        this.catchedFishUI = catchedFishUI;
        this.playerInventory = playerInventory;
        this.ocean = ocean;
    }
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
