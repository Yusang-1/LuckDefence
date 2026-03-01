using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] private CharRank rank;
    [SerializeField] private List<Entity> entities;
    [SerializeField] private int fullCount;

    private List<int> codes;

    private Dictionary<int, Entity> entityAsCodeDict;
    private bool isDirty;

    public CharRank Rank => rank;
    public List<Entity> Entities => entities;
    public List<int> Codes => codes;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;

    public bool IsDirty => isDirty;

    public void Initialize()
    {
        isDirty = false;
        codes = new List<int>();
        entityAsCodeDict = new Dictionary<int, Entity>();

        for (int i = 0; i < entities.Count; i++)
        {
            int code = entities[i].Data.Code;
            codes.Add(code);
            entityAsCodeDict.Add(codes[i], entities[i]);
        }
    }

    public void AddCharacter(Entity entity)
    {
        if(IsCodeExist(entity.Data.Code) == true)
        {
            Debug.LogWarning("이미 존재하는 코드를 추가");
            return;
        }

        Entities.Add(entity);
        //Entities.Sort();

        codes.Add(entity.Data.Code);
        codes.Sort();

        entityAsCodeDict.Add(entity.Data.Code, entity);

        for(int i = 0; i < codes.Count; i++)
        {
            entities[i] = entityAsCodeDict[codes[i]];
        }

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

    public bool isSelectedCharacterFull()
    {
        if(entities.Count == fullCount)
        {
            return true;
        }
        else if(entities.Count > fullCount)
        {
            Debug.LogWarning("배틀 리스트에 캐릭터 초과됨");
            return true;
        }
        else
        {
            return false;
        }
    }
}
