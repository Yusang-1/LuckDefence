using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private BattleTimerUI timerUI;
    [SerializeField] private EnemyCountUI enemyCountUI;
    [SerializeField] private SummonUI summonUI;
    [SerializeField] private StartStageButton startStageButton;
    [SerializeField] private EndStagePanelUI endStagePanelUI;

    [Space]
    [SerializeField] private BattleDataSO battleData;

    public BattleTimerUI TimerUI => timerUI;
    public EnemyCountUI EnemyCountUI => enemyCountUI;
    public StartStageButton StartStageButton => startStageButton;
    public EndStagePanelUI EndStagePanelUI => endStagePanelUI;

    public void Initialize()
    {
        timerUI.Initialize();
        summonUI.Initialize();
        startStageButton.Initialize();

        battleData.EnoughCoin += summonUI.EnableButton;
        battleData.NotEnoughCoin += summonUI.DisableButton;
    }

    private void OnDestroy()
    {
        battleData.EnoughCoin -= summonUI.EnableButton;
        battleData.NotEnoughCoin -= summonUI.DisableButton;
    }

    public void EnableBattleUI()
    {
        loadingUI.SetActive(false);
        battleUI.SetActive(true);
    }
}
