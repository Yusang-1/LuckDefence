using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int Code;
    public string EnemyName;
    public EnemyRank Rank;
    public int MaxMp;
    public float MoveSpeed;
    public GameObject Prefab;
}

public enum EnemyRank
{
    common,
    boss
}