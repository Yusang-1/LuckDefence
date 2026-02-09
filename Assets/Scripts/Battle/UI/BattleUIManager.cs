using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private BattleTimerUI timerUI;
    [SerializeField] private EnemyCountUI enemyCountUI;

    [SerializeField] private BattleDataSO battleData;

    private void Start()
    {
        battleData.StartNextRound += timerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged += enemyCountUI.OnChangeText;
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= timerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged -= enemyCountUI.OnChangeText;
    }
}
