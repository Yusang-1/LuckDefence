using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{    
    [SerializeField] private AbstractFactory[] factories;
    [SerializeField] private RankProbabilitySO probabilityData;
    [SerializeField] private Platforms platforms;

    private CharacterListDataSO charListData;

    private List<CharRank> summonableRanks;
    private List<int> summonableCharacterCodes;
    private List<int> availablePlatformIndex;

    private Dictionary<CharRank, AbstractFactory> factoryDict;

    public IEnumerator Initialize(CharacterListDataSO characterListData)
    {
        charListData = characterListData;
        factoryDict = new Dictionary<CharRank, AbstractFactory>();
        summonableRanks = new List<CharRank>();
        summonableCharacterCodes = new List<int>();
        availablePlatformIndex = new List<int>();

        AbstractFactory factory;
        FactoryChar fc;

        //characterListData.Initialize();

        for (int i = 0; i < factories.Length; i++)
        {
            factory = factories[i];
            fc = factory as FactoryChar;
            factory.Initialize(charListData.CharListAsRankDictionary[fc.Rank].EntityAsCodeDict);

            factoryDict.Add(fc.Rank, factory);

            yield return null;
        }
        probabilityData.Initialize();

        foreach(FactoryChar factor in  factories)
        {
            foreach(var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned += OnCharacterSpawned;
            }
        }
    }

    public void OnDisable()
    {
        foreach (FactoryChar factor in factories)
        {
            foreach (var item in factor.PooledEntityDict)
            {
                item.Value.CharacterSpawned -= OnCharacterSpawned;
            }
        }
    }

    public void OnCharacterSpawned(int platformIndex, GameObject go)
    {
        ResetData();
        platforms.PlatformList[platformIndex].EntitySpawned(go);
    }

    public void ResetData()
    {
        summonableRanks.Clear();
        summonableCharacterCodes.Clear();
        availablePlatformIndex.Clear();
    }

    public void SpawnEntity()
    {
        CharRank rank = CheckSummonableRank();
        int charCode = CheckSummonableCharacterInRank(rank);
        int platformIndex = CheckAvailablePlatformIndexByCharacter(charCode);
        Vector3 position = GetSummonPosition(platformIndex, rank);

        SummonData data = new((int)rank, charCode, platformIndex, position);

        OrderToFactory(data);
    }

    public void OrderToFactory(SummonData data)
    {
        factoryDict[(CharRank)data.CharRank].ActiveEntity(data);
    }

    public CharRank CheckSummonableRank()
    {
        foreach (Platform platform in platforms.PlatformList)
        {
            for (int i = 0; i < (int)CharRank.legendary; i++)
            {
                if (platform.CheckIsRankSummonable((CharRank)i) && !summonableRanks.Contains((CharRank)i))
                {
                    summonableRanks.Add((CharRank)i);
                }
            }

            if (summonableRanks.Count == (int)CharRank.legendary)
            {
                break;
            }
        }

        CharRank rank = CharRank.none;
        float randNum = Random.Range(0, 100);
        float temp = 0;
        foreach (var item in probabilityData.ProbabilityDict)
        {
            temp += item.Value;
            if (randNum <= temp && summonableRanks.Contains(item.Key))
            {
                rank = item.Key;

                break;
            }
        }

        if (rank == CharRank.none)
        {
            rank = summonableRanks[0];
        }

        return rank;
    }

    public int CheckSummonableCharacterInRank(CharRank rank)
    {
        int length = charListData.CharListAsRankDictionary[rank].Entities.Count;
        int code;
        bool isAvailable;

        foreach (Platform platform in platforms.PlatformList)
        {
            for (int i = 0; i < length; i++)
            {
                code = charListData.CharListAsRankDictionary[rank].Entities[i].Data.Code;
                isAvailable = platform.CheckEntityAvailable(code);

                if (isAvailable && !summonableCharacterCodes.Contains(code))
                {
                    summonableCharacterCodes.Add(code);
                }
            }

            if (summonableCharacterCodes.Count == length)
            {
                break;
            }
        }

        int randNum = Random.Range(0, summonableCharacterCodes.Count);

        return summonableCharacterCodes[randNum];
    }

    public int CheckAvailablePlatformIndexByCharacter(int code)
    {
        bool isAvailable;

        foreach (Platform platform in platforms.PlatformList)
        {
            isAvailable = platform.CheckEntityAvailable(code);

            if (isAvailable && !availablePlatformIndex.Contains(platform.Index))
            {
                availablePlatformIndex.Add(platform.Index);
            }
        }

        int randNum = Random.Range(0, availablePlatformIndex.Count);

        return availablePlatformIndex[randNum];
    }

    public Vector3 GetSummonPosition(int index, CharRank rank)
    {
        return platforms.PlatformList[index].GetPosition(rank);
    }

    //public void PromotionEntity(Platform platform) //플렛폼도 인덱스로 할지 고민
    //{
    //    platform.ResetPlatform();
    //    factoryDict[platform.Rank].ActiveEntity(platform);
    //}

    public void PromotionEntity(PlatformData data)
    {
        platforms.PlatformList[data.Index].ResetPlatform();

        CharRank rank = data.Rank + 1;
        int charCode = CheckSummonableCharacterInRank(rank);
        int platformIndex = data.Index;
        Vector3 position = GetSummonPosition(platformIndex, rank);

        SummonData summonData = new((int)rank, charCode, platformIndex, position);

        //Debug.Log($"{rank}, {charCode} Summon");
        factoryDict[(CharRank)summonData.CharRank].ActiveEntity(summonData);
    }
}

public struct SummonData
{
    private readonly int charRank;
    private readonly int charCode;
    private readonly int platformIndex;
    private Vector3 position;

    public SummonData(int charRank, int charCode, int platformIndex, Vector3 position)
    {
        this.charRank = charRank;
        this.charCode = charCode;
        this.platformIndex = platformIndex;
        this.position = position;
    }

    public readonly int CharRank => charRank;
    public readonly int CharCode => charCode;
    public readonly int PlatformIndex => platformIndex;
    public readonly Vector3 Position => position;
}
