using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSO stageData; //임시로 인스펙터에서 받아옴
    [SerializeField] private EnemySpawner enemySpawner;
    int currentRound;

    private void Start()
    {
        Initialize();
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
