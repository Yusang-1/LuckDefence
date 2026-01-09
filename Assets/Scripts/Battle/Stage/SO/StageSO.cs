using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Objects/StageSO")]
public class StageSO : ScriptableObject
{
    [SerializeField] private int stageNum;
    [SerializeField] private RoundData roundData;

    public int StageNum => stageNum;
    public RoundData RoundData => roundData;
}

[Serializable]
public struct RoundData
{
    [SerializeField] private int enemyCode;
    [SerializeField] private int enemyCount;
    [SerializeField] private int time;

    public int EnemyCode => enemyCode;
    public int EnemyCount => enemyCount;
    public int Time => time;
}
