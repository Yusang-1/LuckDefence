using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Scriptable Objects/BattleData")]
public class BattleDataSO : ScriptableObject
{
    public event Action<RoundData> StartNextRound;
    public event Action<int> EnemyCountChanged;
    public event Action EnemyFull;

    [SerializeField] private int roundNum;

    [SerializeField] private int currentEnemyCount;

    private StageSO stageData;

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
            currentEnemyCount = value;
            EnemyCountChanged?.Invoke(currentEnemyCount);

            if (currentEnemyCount > stageData.MaxEnemyCount)
            {
                EnemyFull?.Invoke();
            }
        }
    }

    public void Initialize(StageSO stageData)
    {
        this.stageData = stageData;
        currentEnemyCount = 0;
        RoundNum = -1;
    }
}
