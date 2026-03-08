using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : EntitySO
{
    public EnemyRank Rank;    
}

public enum EnemyRank
{
    common,
    boss
}