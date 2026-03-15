using UnityEngine;
using TMPro;

public class SpeedButtonUI : UIPresenter
{
    [SerializeField] private TextMeshProUGUI speedValueText;
    
    private BattleSpeedController speedController;

    private void Start()
    {
        speedController = FindFirstObjectByType<BattleSpeedController>();
        speedController.GameSpeedChanged += OnUpdateUI;
    }

    private void OnDestroy()
    {
        speedController.GameSpeedChanged -= OnUpdateUI;
    }

    public override void OnUpdateUI<T>(T item)
    {
        ChangeSpeedValueText(item);
    }

    private void ChangeSpeedValueText<T>(T speedValue)
    {
        speedValueText.text = speedValue.ToString();
    }

    // 버튼 할당
    public void OnChangeSpeed()
    {
        speedController.ChangeGameSpeed();
    }       
}
