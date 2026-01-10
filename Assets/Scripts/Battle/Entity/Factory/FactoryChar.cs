using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryChar : AbstractFactory
{
    [SerializeField] private CharRank rank;

    private GameObject[,] pooledCharacters;
    private int[] pooledCount;

    public CharRank Rank => rank;

    public override void Initialize(Dictionary<int, Entity> entityDict)
    {
        base.EntityDict = entityDict;
        int count = EntityDict.Count;

        pooledCharacters = new GameObject[count, pooledNum];
        pooledCount = new int[count];

        GameObject go;
        int num;
        for(int i = 0; i < count; i++)
        {
            num = 0;
            pooledCount[i] = 0;

            foreach(var item in EntityDict)
            {
                go = Instantiate(EntityDict[item.Key].gameObject);
                go.SetActive(false);
                pooledCharacters[i, num] = go;
                num++;
            }
        }
    }

    public override void ActiveEntity(int code, Platform platform)
    {
        int index;
        do
        {
            index = Random.Range(0, EntityDict.Count);
        } while (pooledCount[index] >= pooledNum);
        
        GameObject go = pooledCharacters[index, pooledCount[index]];
        go.SetActive(true);

        Vector3 pos = platform.GetPosition(rank);
        go.transform.position = pos;
        
        pooledCount[index]++;
    }

    public override void DeactiveEntity(int index)
    {
        GameObject go = pooledCharacters[index, pooledCount[index]];
        go.SetActive(false);
        pooledCount[index]--;
    }
}
