using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private BattleDataSO battleData;

    [SerializeField] private StageSO stageData; //임시로 인스펙터에서 받아옴

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private HPSpawner hpSpawner;

    [SerializeField] private BattleTimerUI timerUI;

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        timerUI.TimeIsOver -= StartNextRound;
    }

    public void Initialize()
    {
        battleData.Initialize(stageData);
        battleData.RoundNum = -1;

        hpSpawner.Initialize(stageData);

        battleData.StartNextRound += enemySpawner.SpawnEnemy;
        timerUI.TimeIsOver += StartNextRound;

        StartNextRound();
    }

    private void StartNextRound()
    {
        battleData.RoundNum++;
    }
}
