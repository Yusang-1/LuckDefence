using UnityEngine;
using TMPro;

public class SpeedButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedValueText;
    
    private BattleSpeedController speedController;

    private void Start()
    {
        speedController = FindFirstObjectByType<BattleSpeedController>();
        speedController.GameSpeedChanged += OnChangeSpeedValueText;
    }

    // 버튼에 할당
    public void OnChangeSpeed()
    {
        speedController.MakeGameFaster();
    }

    public void OnChangeSpeedValueText(int speedValue)
    {
        speedValueText.text = speedValue.ToString();
    }
}
