using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;
    [SerializeField] private RankProbabilitySO probabilityData;
    [SerializeField] private CharListAsRank[] charListAsRanks;
    [SerializeField] private Platforms platforms;

    private List<CharRank> summonableRanks;
    private List<int> summonableCharacterCodes;
    private List<int> availablePlatformIndex;

    private Dictionary<CharRank, AbstractFactory> factoryDict;
    private Dictionary<int, Entity> entityAsCodeDict;

    private void Start()
    {
        factoryDict = new Dictionary<CharRank, AbstractFactory>();
        summonableRanks = new List<CharRank>();
        summonableCharacterCodes = new List<int>();
        availablePlatformIndex = new List<int>();

        Initialize();
    }

    public void Initialize()
    {
        AbstractFactory factory;
        FactoryChar fc;
        CharListAsRank charListAsRank;
        Entity entity;
        
        for (int i = 0; i < charListAsRanks.Length; i++)
        {
            entityAsCodeDict = new Dictionary<int, Entity>();

            charListAsRank = charListAsRanks[i];

            for (int j = 0; j < charListAsRank.Entities.Length; j++)
            {
                entity = charListAsRank.Entities[j];
                int code = entity.Data.Code;
                entityAsCodeDict.Add(code, entity);
            }

            factory = factories[i];
            fc = factory as FactoryChar;
            factoryDict.Add(fc.Rank, factory);

            factory.Initialize(entityAsCodeDict);
            probabilityData.Initialize();
        }

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

        SummonData data = new SummonData((int)rank, charCode, platformIndex, position);

        //Debug.Log($"{rank}, {charCode} Summon");
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
        int length = charListAsRanks[(int)rank].Entities.Length;
        int code;
        bool isAvailable;

        foreach (Platform platform in platforms.PlatformList)
        {
            for (int i = 0; i < length; i++)
            {
                code = charListAsRanks[(int)rank].Entities[i].Data.Code;
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

    public void PromotionEntity()
    {

    }
}

public struct SummonData
{
    private int charRank;
    private int charCode;
    private int platformIndex;
    private Vector3 position;

    public SummonData(int charRank, int charCode, int platformIndex, Vector3 position)
    {
        this.charRank = charRank;
        this.charCode = charCode;
        this.platformIndex = platformIndex;
        this.position = position;
    }

    public int CharRank => charRank;
    public int CharCode => charCode;
    public int PlatformIndex => platformIndex;
    public Vector3 Position => position;
}
