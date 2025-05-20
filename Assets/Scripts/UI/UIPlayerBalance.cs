using TMPro;
using UnityEngine;
using VContainer;
using DG.Tweening;
public class UIPlayerBalance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceTxt;
    [SerializeField] private RectTransform parentRt;
    [Inject] private PlayerMoneyBalance playerBalance;
    void Start()
    {
        playerBalance.OnMoneyAdded += () => { UpdateBalance(false); };
        playerBalance.OnMoneyDiscarded += () => { UpdateBalance(true); };

        balanceTxt.SetText(playerBalance.GetCurrentBalance().ToString());
    }
    private void UpdateBalance(bool isDiscard)
    {
        // Determin where to "punch" animation depending on money update type
        int punchDir = isDiscard ? -1 : 1;

        DOTween.Kill(parentRt);

        parentRt.localScale = Vector3.one;

        parentRt.DOPunchScale(Vector3.one * 0.2f * punchDir, 0.3f, 5, 0.2f);

        balanceTxt.SetText(playerBalance.GetCurrentBalance().ToString());
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerBalance.AddMoney(20);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            playerBalance.DiscardMoney(10);
        }
    }
#endif
}
