using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public int Code;
    public string CharacterName;
    public CharRank Rank;
    public int MaxMp;
    public float AttackSpeed;
    public float MoveSpeed;
    public GameObject Prefab;
}
