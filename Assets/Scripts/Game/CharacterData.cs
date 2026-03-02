using UnityEngine;
using System.Collections.Generic;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private CharacterListDataSO characterListData;
    [SerializeField] private CharacterListDataSO ownedCharacterListData;
    [SerializeField] private CharacterListDataSO selectedCharacterListData;

    [SerializeField] int codeUnit;

    [SerializeField] private int commonCount;
    [SerializeField] private int rareCount;
    [SerializeField] private int epicCount;
    [SerializeField] private int uniqueCount;
    [SerializeField] private int legendaryCount;
    [SerializeField] private int allCount;
    private Dictionary<CharRank, int> rankCountByRank;

    public CharacterListDataSO CharacterListData => characterListData;
    public CharacterListDataSO OwnedCharacterListData => ownedCharacterListData;
    public CharacterListDataSO SelectedCharacterListData => selectedCharacterListData;

    public int AllCount => allCount;
    public Dictionary<CharRank, int> RankCountByRank => rankCountByRank;

    private void Start()
    {
        characterListData.Initialize();
        ownedCharacterListData.Initialize();
        selectedCharacterListData.Initialize();

        rankCountByRank = new Dictionary<CharRank, int>();
        rankCountByRank.Add(CharRank.common, commonCount);
        rankCountByRank.Add(CharRank.rare, rareCount);
        rankCountByRank.Add(CharRank.epic, epicCount);
        rankCountByRank.Add(CharRank.unique, uniqueCount);
        rankCountByRank.Add(CharRank.legendary, legendaryCount);
    }

    public void AddOwnedCharacter(Entity entity)
    {
        ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].AddCharacter(entity);
    }

    public void RemoveOwnedCharacter(Entity entity)
    {
        ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].RemoveCharacter(entity.Data.Code);

        if(selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].IsCodeExist(entity.Data.Code) == true)
        {
            RemoveSelectedCharacter(entity);
        }
    }

    public void AddSelectedCharacter(Entity entity)
    {
        if (ownedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].IsCodeExist(entity.Data.Code) == false)
        {
            Debug.LogWarning("소유하지 않은 캐릭터를 엔트리에 넣으려고 시도");
            return;
        }

        selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].AddCharacter(entity);
        selectedCharacterListData.IsDirty = true;
    }

    public void RemoveSelectedCharacter(Entity entity)
    {
        selectedCharacterListData.CharListAsRankDictionary[GetCharRankByCode(entity.Data.Code)].RemoveCharacter(entity.Data.Code);
    }

    public CharRank GetCharRankByCode(int code)
    {
        return (CharRank)(code / codeUnit - 1);
    }

    public bool isSelectedCharacterFull()
    {
        int count = 0;
        foreach(var item in selectedCharacterListData.CharListAsRankDictionary)
        {
            if(item.Value.isSelectedCharacterFull() == true)
            {
                count++;
            }
        }

        if(count == selectedCharacterListData.CharListAsRankDictionary.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
