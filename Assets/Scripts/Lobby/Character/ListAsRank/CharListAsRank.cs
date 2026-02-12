using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] private CharRank rank;
    [SerializeField] private Entity[] entities;

    private Dictionary<int, Entity> entityAsCodeDict;

    public CharRank Rank => rank;
    public Entity[] Entities => entities;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;

    public void Initialize()
    {
        entityAsCodeDict = new Dictionary<int, Entity>();

        for (int i = 0; i < entities.Length; i++)
        {
            int code = entities[i].Data.Code;
            entityAsCodeDict.Add(code, entities[i]);
        }
    }
}
