using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    private BattleUIManager battleUIManager;

    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    private HPSpawner hpSpawner;

    [SerializeField] private StageSO stageData;
    [SerializeField] private BattleDataSO battleData;
    [SerializeField] private CharacterListDataSO charListData;
    [SerializeField] private EnemyList enemyList;

    [SerializeField] private Platforms platforms;

    public IEnumerator Start()
    {
        battleUIManager = FindFirstObjectByType<BattleUIManager>();
        hpSpawner = FindFirstObjectByType<HPSpawner>();

        //Debug.Log("Start BattleManager Start");
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
        
        battleUIManager.EndStagePanelUI.RetryStage += enemyList.OnDeactivateAllEnemy;
        battleUIManager.EndStagePanelUI.RetryStage += hpSpawner.OnDeactiveAllHP;
        battleUIManager.EndStagePanelUI.RetryStage += battleUIManager.TimerUI.OnResetTimer;
        battleUIManager.EndStagePanelUI.RetryStage += battleUIManager.EnemyCountUI.OnReset;
        battleUIManager.EndStagePanelUI.RetryStage += battleUIManager.StartStageButton.OnOpenUI;
        battleUIManager.EndStagePanelUI.RetryStage += battleUIManager.EndStagePanelUI.OnDeactivePanel;
        battleUIManager.EndStagePanelUI.RetryStage += battleData.OnResetData;

        yield return null;

        yield return characterSpawner.Initialize(charListData);

        battleUIManager.EnableBattleUI();
        //Debug.Log("Finish BattleManager Start");
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
        battleUIManager.EndStagePanelUI.RetryStage -= enemyList.OnDeactivateAllEnemy;
        battleUIManager.EndStagePanelUI.RetryStage -= hpSpawner.OnDeactiveAllHP;
        battleUIManager.EndStagePanelUI.RetryStage -= battleUIManager.TimerUI.OnResetTimer;
        battleUIManager.EndStagePanelUI.RetryStage -= battleUIManager.EnemyCountUI.OnReset;
        battleUIManager.EndStagePanelUI.RetryStage -= battleUIManager.StartStageButton.OnOpenUI;
        battleUIManager.EndStagePanelUI.RetryStage -= battleUIManager.EndStagePanelUI.OnDeactivePanel;
        battleUIManager.EndStagePanelUI.RetryStage -= battleData.OnResetData;
    }

    public void StartBattle()
    {
        stageManager.StartNextRound();
    }
    
    private void CheckEnemyNum()
    {

    }

    private void ResetStage()
    {
        
    }

    private void GameOver()
    {
        battleData.IsGameOver = true;
    }
}

//플렛폼에 프로모션 missing