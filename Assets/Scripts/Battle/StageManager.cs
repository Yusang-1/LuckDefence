using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private BattleDataSO battleData;

    [SerializeField] private BattleTimerUI timerUI;

    public void StartNextRound()
    {
        battleData.RoundNum++;
    }
}
