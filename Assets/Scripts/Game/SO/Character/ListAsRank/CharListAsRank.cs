using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] private CharRank rank;
    [SerializeField] private Entity[] entities;

    private Dictionary<int, Entity> entityAsCodeDict;
    [SerializeField] private bool isDirty;

    public CharRank Rank => rank;
    public Entity[] Entities => entities;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;

    public bool IsDirty => isDirty;

    public void Initialize()
    {
        isDirty = false;
        entityAsCodeDict = new Dictionary<int, Entity>();

        for (int i = 0; i < entities.Length; i++)
        {
            int code = entities[i].Data.Code;
            entityAsCodeDict.Add(code, entities[i]);
        }
    }

    public void AddCharacter(Entity entity)
    {
        if(IsCodeExist(entity.Data.Code) == true)
        {
            Debug.LogWarning("이미 존재하는 코드를 추가");
            return;
        }

        entityAsCodeDict.Add(entity.Data.Code, entity);
        isDirty = true;
    }

    public void RemoveCharacter(int code)
    {
        if (IsCodeExist(code) == false)
        {
            Debug.LogWarning("존재하지 않는 코드를 제거");
            return;
        }

        entityAsCodeDict.Remove(code);
        isDirty = true;
    }

    public bool IsCodeExist(int code)
    {
        return entityAsCodeDict.ContainsKey(code);
    }

    public void SetDirty(bool value)
    {
        isDirty = value;
    }
}
