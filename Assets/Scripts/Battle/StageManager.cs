using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private BattleDataSO battleData;

    [SerializeField] private BattleTimerUI timerUI;

    private void Start()
    {
        timerUI = FindFirstObjectByType<BattleTimerUI>();
    }

    public void StartNextRound()
    {
        battleData.RoundNum++;
    }
}
