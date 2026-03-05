using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "SelectedCharListAsRank", menuName = "Scriptable Objects/SelectedCharListAsRank")]
public class SelectedCharListAsRank : CharListAsRank
{
    //[SerializeField] private int fullCount;

    // 숫자가 높은 것부터 대체
    [SerializeField] private int[] changePriority;

    public override void Initialize()
    {
        base.Initialize();

        changePriority = new int[fullCount];
    }

    public override void AddCharacter(Entity entity)
    {
        if (IsCodeExist(entity.Data.Code) == true)
        {
            Debug.LogWarning("이미 존재하는 코드를 추가");
            return;
        }

        int listFilled = 0;
        for(int i = 0; i < entityList.Length; i++)
        {
            if (entityList[i] != null)
            {
                listFilled++;
            }
        }

        // 해당 랭크의 티오가 다 찼을 경우 가장 오래전에 추가됐던 캐릭터를 대체
        int index;
        if(listFilled == fullCount)
        {
            index = Array.IndexOf<int>(changePriority, changePriority.Max<int>());

            entityAsCodeDict.Remove(codeList[index]);

            codeList[index] = entity.Data.Code;
            entityList[index] = entity;
            entityAsCodeDict.Add(entity.Data.Code, entity);
        }
        else
        {
            base.AddCharacter(entity); //정렬되지 않게

            index = Array.IndexOf<int>(codeList, entity.Data.Code);
        }
            
        // 새로 추가된 index의 priority는 1로 나머지 priority는 1을 추가        
        changePriority[index] = 1;
        for (int i = 0; i < changePriority.Length; i++)
        {
            if(i == index || changePriority[i] == 0)
            {
                continue;
            }

            changePriority[i]++;
        }
        
        isDirty = true;
    }

    public bool isSelectedCharacterFull()
    {
        if (entityList.Length == fullCount)
        {
            return true;
        }
        else if (entityList.Length > fullCount)
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
