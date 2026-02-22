using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharListAsRank", menuName = "Scriptable Objects/CharListAsRank")]
public class CharListAsRank : ScriptableObject
{
    [SerializeField] private CharRank rank;
    [SerializeField] private Entity[] entities;

    private Dictionary<int, Entity> entityAsCodeDict;
    private Dictionary<int, bool> isEntityOwnedDict;

    [SerializeField] private List<int> selectedCharCodeList;

    public CharRank Rank => rank;
    public Entity[] Entities => entities;
    public Dictionary<int, Entity> EntityAsCodeDict => entityAsCodeDict;
    public Dictionary<int, bool> IsEntityOwnedDict => isEntityOwnedDict;
    public List<int> SelectedCharCodeList => selectedCharCodeList;

    public void Initialize()
    {
        entityAsCodeDict = new Dictionary<int, Entity>();
        isEntityOwnedDict = new Dictionary<int, bool>();
        selectedCharCodeList = new List<int>();

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

    public void AddCharacterInBattleList(int code)
    {
        if (isEntityOwnedDict[code] == false)
        {
            Debug.LogWarning("소유하지 않은 캐릭터를 배틀리스트에 추가 시도");
            return;
        }

        if(selectedCharCodeList.Contains(code) == false)
        {
            selectedCharCodeList.Add(code);
        }
    }

    public void RemoveCharacterInBattleList(int code)
    {
        if (selectedCharCodeList.Contains(code) == true)
        {
            selectedCharCodeList.Remove(code);
        }
    }
}
