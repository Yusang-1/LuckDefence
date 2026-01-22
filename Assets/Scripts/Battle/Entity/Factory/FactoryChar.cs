using UnityEngine;
using System.Collections.Generic;

public class FactoryChar : AbstractFactory
{
    [SerializeField] private CharRank rank;    

    //protected PooledEntity pooledEntity;
    protected Dictionary<int, PooledEntity> pooledEntityDict;
    protected List<int> availableCodeList;

    //[SerializeField] private Platforms platforms;

    public CharRank Rank => rank;
    public Dictionary<int, PooledEntity> PooledEntityDict => pooledEntityDict;

    public override void Initialize(Dictionary<int, Entity> entityDict)
    {
        pooledEntityDict = new Dictionary<int, PooledEntity>();
        availableCodeList = new List<int>();

        //EntityDict = entityDict;
        PooledEntity pooledEntity;
        GameObject[] gameObjects;
        GameObject go;
        int poolNum;

        foreach (var item in entityDict)
        {
            poolNum = (item.Value.Data as CharacterSO).poolNum;
            gameObjects = new GameObject[poolNum];

            for (int i = 0; i < poolNum; i++)
            {
                go = Instantiate(item.Value.gameObject);
                go.SetActive(false);
                gameObjects[i] = go;
            }

            pooledEntity = new PooledEntity(gameObjects, (item.Value.Data as CharacterSO).poolNum);
            pooledEntityDict.Add(item.Key, pooledEntity);
            availableCodeList.Add(item.Key);
        }
    }

    //public override void ActiveEntity()
    //{
    //    int code = DetermineEntityCode();

    //    Platform platform = SearchPlatform(code);
    //    Vector3 position = platform.GetPosition(RankByCharCodeDict[code]);

    //    bool isPoolFull = pooledEntityDict[code].ActiveEntity(position);

    //    if (isPoolFull && availableCodeList.Contains(code))
    //    {
    //        availableCodeList.Remove(code);
    //    }

    //    platform.EntitySpawned(pooledEntityDict[code].GetLastActivatedEntity());
    //}

    public override void ActiveEntity(SummonData data)
    {
        Vector3 position = data.Position;
        pooledEntityDict[data.CharCode].ActiveEntity(data);
    }

    //public override void ActiveEntity(Platform platform) //인덱스로 추후 수정
    //{
    //    int code = DetermineEntityCode();

    //    Vector3 position = platform.GetPosition(RankByCharCodeDict[code]);

    //    bool isPoolFull = pooledEntityDict[code].ActiveEntity(position);

    //    if (isPoolFull && availableCodeList.Contains(code))
    //    {
    //        availableCodeList.Remove(code);
    //    }

    //    platform.EntitySpawned(pooledEntityDict[code].GetLastActivatedEntity());
    //}

    public override void DeactiveEntity(int code)
    {
        pooledEntityDict[code].DeactiveEntity();

        if (availableCodeList.Contains(code) == false)
        {
            availableCodeList.Add(code);
        }
    }

    //public virtual Platform SearchPlatform(int code)
    //{
    //    List<int> availablePlatformsCode = new List<int>();

    //    int num = 0;
    //    foreach(Platform platform in platforms.PlatformList)
    //    {
    //        if(platform.CheckEntityAvailable(code))
    //        {
    //            availablePlatformsCode.Add(num);
    //        }
    //        num++;
    //    }
        
    //    num = Random.Range(0, availablePlatformsCode.Count);
    //    return platforms.PlatformList[availablePlatformsCode[num]];
    //}

    //public virtual int DetermineEntityCode()
    //{
    //    int randNum = Random.Range(0, availableCodeList.Count);

    //    try
    //    {
    //        return availableCodeList[randNum];
    //    }
    //    catch
    //    {
    //        Debug.LogError($"{gameObject.name}, {availableCodeList}");
    //    }
    //    return availableCodeList[randNum];
    //}
}
