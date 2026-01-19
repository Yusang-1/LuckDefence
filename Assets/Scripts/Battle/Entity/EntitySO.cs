using UnityEngine;

[CreateAssetMenu(fileName = "EntitySO", menuName = "Scriptable Objects/EntitySO")]
public class EntitySO : ScriptableObject
{
    public int Code;
    public string EntityName;
    public int MaxHp;
    public int MaxMp;
    public float AttackSpeed;
    public float MoveSpeed;
    public GameObject Prefab;
}
