using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] private CharRank rank;
    [SerializeField] private Entity[] entities;

    private Dictionary<int, Entity> entityAsCodeDict;
    private Dictionary<int, bool> isEntityOwnedDict;

    public CharRank Rank => rank;
    public Entity[] Entities => entities;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;
    public Dictionary<int, bool> IsEntityOwnedDict => isEntityOwnedDict;

    public void Initialize()
    {
        entityAsCodeDict = new Dictionary<int, Entity>();
        isEntityOwnedDict = new Dictionary<int, bool>();

        for (int i = 0; i < entities.Length; i++)
        {
            int code = entities[i].Data.Code;
            entityAsCodeDict.Add(code, entities[i]);
            IsEntityOwnedDict.Add(code, false);
        }
    }

    public void GetCharacter(int code)
    {
        if (IsEntityOwnedDict[code] == false)
        {
            IsEntityOwnedDict[code] = true;
        }
    }
}
