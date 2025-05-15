using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    public event Action OnExit;
    [SerializeField] private List<FishSO> fishList = new List<FishSO>();
    [Tooltip("Minimum time to find a fish")]
    [SerializeField] private float minTimeToCatch = 3f;
    [Tooltip("Maximum time to find a fish")]
    [SerializeField] private float maxTimeToCatch = 5f;
    [Header("Components")]
    [SerializeField] private UIFishing fishingUI;
    [SerializeField] private UIFishingBar fishingBarUI;
    [SerializeField] private Ocean ocean;

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
        yield return new WaitForSeconds(timeToCatch);

        fishingBarUI.PrepareFishing(FishStrength.VeryWeak, FishSpeed.VerySlow);
        fishingBarUI.ShowUI();
        isFishing = true;
    }

    private void CatchFish()
    {
        if (!isFishing) return;

        bool isSuccess = fishingBarUI.CatchFish();

        Debug.Log("Fish catched = " + isSuccess);

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
