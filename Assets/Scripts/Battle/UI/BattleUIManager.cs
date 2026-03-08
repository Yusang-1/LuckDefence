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
    }

    public void EnableBattleUI()
    {
        loadingUI.SetActive(false);
        battleUI.SetActive(true);
    }
}
