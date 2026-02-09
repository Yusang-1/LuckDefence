using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSO stageData; //임시로 인스펙터에서 받아옴
    [SerializeField] private StageSO testStage;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private HPSpawner hpSpawner;

    [SerializeField] private BattleTimerUI timerUI;
    int currentRound;

    private void Start()
    {
        Initialize();
        hpSpawner.Initialize(stageData);
        timerUI.TimeIsOver += StartNextRound;

        StartNextRound();
    }

    private void OnDestroy()
    {
        timerUI.TimeIsOver -= StartNextRound;
    }

    public void Initialize()
    {
        currentRound = 0;
    }

    private void StartNextRound()
    {
        enemySpawner.SpawnEnemy(stageData.RoundData[currentRound]);
        timerUI.AddTime(stageData.RoundData[currentRound].AdditionalTime);

        currentRound++;
    }
}
