using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VContainer;

public class UICatchedFishIcon : MonoBehaviour
{
    [SerializeField] private Transform modelSpotTransform;
    [Inject] private FishObjectPool fishObjectPool;
    private FishItem currentFish;
    private Tween rotationTween;
    public void SetIcon(Fish fish, float animationLength)
    {
        if (currentFish != null) RemovePreviousIcon();

        currentFish = fishObjectPool.GetFish(fish);

        currentFish.transform.SetParent(modelSpotTransform);
        currentFish.transform.localPosition = Vector3.zero;
        currentFish.transform.rotation = Quaternion.identity;

        currentFish.renderer.material = fish.fishMaterial;

        modelSpotTransform.localRotation = Quaternion.identity;

        if (rotationTween != null && rotationTween.IsActive())
            rotationTween.Kill();

        float rotationDuration = 4f;
        int loopsCount = Mathf.FloorToInt(animationLength / rotationDuration) + 1;

        rotationTween = modelSpotTransform
            .DOLocalRotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(loopsCount, LoopType.Incremental);
    }

    private void RemovePreviousIcon()
    {
        if (rotationTween != null && rotationTween.IsActive())
            rotationTween.Kill();

        currentFish.transform.SetParent(null);

        fishObjectPool.ReturnFish(currentFish);
    }
}
