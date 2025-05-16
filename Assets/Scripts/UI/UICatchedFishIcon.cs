using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UICatchedFishIcon : MonoBehaviour
{
    [SerializeField] private Transform modelSpotTransform;
    private Transform currentModel;
    private Tween rotationTween;
    public void SetIcon(Transform model)
    {
        if (currentModel != null) Destroy(currentModel.gameObject);

        currentModel = Instantiate(model, modelSpotTransform);

        modelSpotTransform.localRotation = Quaternion.identity;

        if (rotationTween != null && rotationTween.IsActive())
            rotationTween.Kill();

        rotationTween = modelSpotTransform
            .DOLocalRotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }
}
