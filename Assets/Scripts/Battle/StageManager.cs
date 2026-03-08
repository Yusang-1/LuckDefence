using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private BattleDataSO battleData;

    private BattleTimerUI timerUI;

    private void Start()
    {
        timerUI = FindFirstObjectByType<BattleTimerUI>();
    }

    public void StartNextRound()
    {
        if(battleData.RoundNum == battleData.StageData.RoundCount - 1 || battleData.IsGameOver)
        {
            return;
        }

        battleData.RoundNum++;
    }
}
