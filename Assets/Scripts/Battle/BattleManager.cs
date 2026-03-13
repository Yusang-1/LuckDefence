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

    public IEnumerator Start()
    {
        battleUIManager = FindFirstObjectByType<BattleUIManager>();
        hpSpawner = FindFirstObjectByType<HPSpawner>();

        battleData.Initialize(stageData);
        enemySpawner.Initialize(stageData.RoundData);
        hpSpawner.Initialize(stageData);
        battleUIManager.Initialize();
        battleUIManager.EnemyCountUI.Initialize(stageData.MaxEnemyCount);

        battleData.StartNextRound += enemySpawner.SpawnEnemy;
        battleData.StartNextRound += battleUIManager.TimerUI.OnStartTimerAddTime;

        battleData.EnemyCountChanged += battleUIManager.EnemyCountUI.OnChangeText;

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

        battleUIManager.TimerUI.TimeIsOver += stageManager.StartNextRound;

        battleUIManager.EndStagePanelUI.RetryStage += OnRestartBattle;
        battleUIManager.EscMenuUI.RetryStage += OnRestartBattle;

        yield return null;

        yield return characterSpawner.Initialize(charListData);

        battleUIManager.EnableBattleUI();
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        battleData.StartNextRound -= battleUIManager.TimerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged -= battleUIManager.EnemyCountUI.OnChangeText;
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
        battleUIManager.TimerUI.TimeIsOver -= stageManager.StartNextRound;
        battleUIManager.EndStagePanelUI.RetryStage -= OnRestartBattle;
    }

    public void StartBattle()
    {
        stageManager.StartNextRound();
    }

    public void OnRestartBattle()
    {
        enemyList.OnDeactivateAllEnemy();
        hpSpawner.OnDeactiveAllHP();
        battleUIManager.ResetBattleUI();
        battleData.OnResetData();
    }

    private void GameOver()
    {
        battleData.IsGameOver = true;
    }
}
