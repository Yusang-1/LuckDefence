using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private BattleUIManager UIManager;

    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private HPSpawner hpSpawner;

    [SerializeField] private StageSO stageData;
    [SerializeField] private BattleDataSO battleData;
    [SerializeField] private CharacterListDataSO charListData;

    public IEnumerator Start()
    {
        battleData.Initialize(stageData);

        hpSpawner.Initialize(stageData);

        battleData.StartNextRound += enemySpawner.SpawnEnemy;

        battleData.StartNextRound += UIManager.TimerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged += UIManager.EnemyCountUI.OnChangeText;

        UIManager.TimerUI.TimeIsOver += stageManager.StartNextRound;

        yield return null;

        yield return characterSpawner.Initialize(charListData);

        UIManager.EnableBattleUI();
    }

    private void OnDestroy()
    {
        battleData.StartNextRound -= enemySpawner.SpawnEnemy;
        battleData.StartNextRound -= UIManager.TimerUI.OnStartTimerAddTime;
        battleData.EnemyCountChanged -= UIManager.EnemyCountUI.OnChangeText;
        UIManager.TimerUI.TimeIsOver -= stageManager.StartNextRound;
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