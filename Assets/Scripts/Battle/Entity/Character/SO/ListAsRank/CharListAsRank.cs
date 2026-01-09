using UnityEngine;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] CharRank rank;
    [SerializeField] Entity[] entities;

    public CharRank Rank => rank;
    public Entity[] Entities => entities;
}
