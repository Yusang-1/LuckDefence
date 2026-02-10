using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private BattleTimerUI timerUI;
    [SerializeField] private EnemyCountUI enemyCountUI;

    [SerializeField] private BattleDataSO battleData;

    public BattleTimerUI TimerUI => timerUI;
    public EnemyCountUI EnemyCountUI => enemyCountUI;

    public void EnableBattleUI()
    {
        loadingUI.SetActive(false);
        battleUI.SetActive(true);
    }
}
