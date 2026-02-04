using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSO stageData; //임시로 인스펙터에서 받아옴
    [SerializeField] private StageSO testStage;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private HPSpawner hpSpawner;
    int currentRound;

    private void Start()
    {
        Initialize();
        hpSpawner.Initialize(stageData);

        StartNextRound();
    }

    public void Initialize()
    {
        currentRound = 0;
    }

    private void StartNextRound()
    {
        enemySpawner.SpawnEnemy(stageData.RoundData[currentRound]);

        currentRound++;
    }
}
