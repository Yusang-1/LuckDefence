using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Scriptable Objects/BattleData")]
public class BattleDataSO : ScriptableObject
{
    public event Action<RoundData> StartNextRound;
    public event Action<int> EnemyCountChanged;
    public event Action EnemyFull;
    public event Action AllEnemyDied;

    [SerializeField] private int roundNum;

    [SerializeField] private int currentEnemyCount;

    private StageSO stageData;

    public StageSO StageData => stageData;

    public bool IsGameOver;

    public int RoundNum
    {
        get => roundNum;
        set
        {
            roundNum = value;

            if(roundNum >= 0)
            {
                StartNextRound?.Invoke(stageData.RoundData[roundNum]);
            }
        }
    }

    public int CurrentEnemyCount
    {
        get => currentEnemyCount;
        set
        {
            if (currentEnemyCount > stageData.MaxEnemyCount + 1)
            {
                return;
            }

            currentEnemyCount = value;
            EnemyCountChanged?.Invoke(currentEnemyCount);

            if (currentEnemyCount > stageData.MaxEnemyCount)
            {
                EnemyFull?.Invoke();
            }

            if(roundNum == stageData.RoundCount - 1 && currentEnemyCount == 0)
            {
                AllEnemyDied?.Invoke();
            }
        }
    }

    public void Initialize(StageSO stageData)
    {
        IsGameOver = false;
        this.stageData = stageData;
        currentEnemyCount = 0;
        RoundNum = -1;
        EnemyList.EnemyDied += OnEnemyDied;
    }

    public void OnResetData()
    {
        IsGameOver = false;
        currentEnemyCount = 0;
        RoundNum = -1;
    }

    private void OnEnemyDied()
    {
        currentEnemyCount--;
    }
}
