using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private StageManager stageManager;
    private BattleUIManager battleUIManager;

    [Header("Spawners")]
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    private HPSpawner hpSpawner;

    [Header("Datas")]
    [SerializeField] private StageSO stageData;
    [SerializeField] private BattleDataSO battleData;
    [SerializeField] private CharacterListDataSO charListData;
    [SerializeField] private EnemyList enemyList;

    [Space]
    [SerializeField] private Platforms platforms;
    [SerializeField] private BattleSpeedController speedController;
    [SerializeField] private BattleTimer battleTimer;

    public IEnumerator Start()
    {
        battleUIManager = FindFirstObjectByType<BattleUIManager>();
        hpSpawner = FindFirstObjectByType<HPSpawner>();

        battleData.Initialize(stageData);
        speedController.Initialize();
        enemySpawner.Initialize(stageData.RoundData);
        hpSpawner.Initialize(stageData);
        battleUIManager.Initialize(stageData);
        battleTimer.Initialize();

        battleData.StartNextRound += enemySpawner.SpawnEnemy;
        battleData.StartNextRound += battleTimer.OnStartTimerAddTime;

        battleData.EnemyFull += battleUIManager.EndStagePanelUI.OnShowGameOverPanel;
        battleData.EnemyFull += enemySpawner.OnStopActiveCoroutine;
        battleData.EnemyFull += GameOver;
        foreach (var platform in platforms.PlatformList)
        {
            battleData.EnemyFull += platform.ResetPlatform;
        }

        battleData.AllEnemyDied += battleUIManager.EndStagePanelUI.OnShowStageClearPanel;
        foreach(var platform in platforms.PlatformList)
        {
            battleData.AllEnemyDied += platform.ResetPlatform;
        }

        battleTimer.TimeIsOver += stageManager.StartNextRound;

        battleUIManager.EndStagePanelUI.RetryStage += OnRestartBattle;
        battleUIManager.EscMenuUI.RetryStage += OnRestartBattle;

        yield return null;

        yield return characterSpawner.Initialize(charListData);

        battleUIManager.EnableBattleUI();
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        battleData.StartNextRound -= battleTimer.OnStartTimerAddTime;
        battleData.EnemyFull -= battleUIManager.EndStagePanelUI.OnShowGameOverPanel;
        battleData.EnemyFull -= enemySpawner.OnStopActiveCoroutine;
        foreach (var platform in platforms.PlatformList)
        {
            battleData.EnemyFull -= platform.ResetPlatform;
        }

        battleData.AllEnemyDied -= battleUIManager.EndStagePanelUI.OnShowStageClearPanel;
        foreach (var platform in platforms.PlatformList)
        {
            battleData.AllEnemyDied -= platform.ResetPlatform;
        }
        battleTimer.TimeIsOver -= stageManager.StartNextRound;
        battleUIManager.EndStagePanelUI.RetryStage -= OnRestartBattle;
        battleUIManager.EscMenuUI.RetryStage -= OnRestartBattle;
    }

    public void StartBattle()
    {
        stageManager.StartNextRound();
    }

    public void OnRestartBattle()
    {
        enemyList.OnDeactivateAllEnemy();
        enemySpawner.ResetSpawner();
        hpSpawner.OnDeactiveAllHP();
        battleData.OnResetData();
        speedController.Initialize();
        battleUIManager.ResetBattleUI();
    }

    private void GameOver()
    {
        battleData.IsGameOver = true;
    }
}
