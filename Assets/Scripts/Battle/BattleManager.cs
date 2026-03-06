using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private BattleUIManager battleUIManager;

    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    private HPSpawner hpSpawner;

    [SerializeField] private StageSO stageData;
    [SerializeField] private BattleDataSO battleData;
    [SerializeField] private CharacterListDataSO charListData;

    public IEnumerator Start()
    {
        battleUIManager = FindFirstObjectByType<BattleUIManager>();
        hpSpawner = FindFirstObjectByType<HPSpawner>();

        Debug.Log("Start BattleManager Start");
        battleData.Initialize(stageData);

        hpSpawner.Initialize(stageData);

        battleData.StartNextRound += enemySpawner.SpawnEnemy;

        battleData.StartNextRound += battleUIManager.TimerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged += battleUIManager.EnemyCountUI.OnChangeText;

        battleUIManager.TimerUI.TimeIsOver += stageManager.StartNextRound;

        yield return null;

        yield return characterSpawner.Initialize(charListData);

        battleUIManager.EnableBattleUI();
        Debug.Log("Finish BattleManager Start");
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        battleData.StartNextRound -= battleUIManager.TimerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged -= battleUIManager.EnemyCountUI.OnChangeText;
        battleUIManager.TimerUI.TimeIsOver -= stageManager.StartNextRound;
    }

    public void StartBattle()
    {
        stageManager.StartNextRound();
    }
    
    private void CheckEnemyNum()
    {

    }

    private void GameOver()
    {

    }
}