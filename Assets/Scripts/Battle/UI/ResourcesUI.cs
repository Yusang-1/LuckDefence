using UnityEngine;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI jewlText;

    [SerializeField] private BattleDataSO battleData;

    private void Start()
    {
        battleData.CoinChanged += UpdateCoinUI;
        UpdateCoinUI();
    }

    private void OnDestroy()
    {
        battleData.CoinChanged -= UpdateCoinUI;
    }

    private void UpdateCoinUI()
    {
        coinText.text = battleData.CurrentCoin.ToString();
    }
}
