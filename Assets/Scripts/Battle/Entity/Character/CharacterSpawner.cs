using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;
    [SerializeField] private Platform[] platforms;
    [SerializeField] private CharListAsRank[] charListAsRanks;

    private Dictionary<CharRank, AbstractFactory> factoryDict;

    private Dictionary<int, Entity> entityAsCodeDict;
    private Dictionary<CharRank, Dictionary<int, Entity>> charListAsRankDict;
    private Dictionary<CharRank, int[]> codeListDict;

    private void Start()
    {
        factoryDict         = new Dictionary<CharRank, AbstractFactory>();
        charListAsRankDict  = new Dictionary<CharRank, Dictionary<int, Entity>>();
        codeListDict        = new Dictionary<CharRank, int[]>();

        Initialize();
    }

    public void Initialize()
    {
        AbstractFactory factory;
        FactoryChar fc;
        CharListAsRank charListAsRank;
        Entity entity;
        Character character;
        int[] codeList;

        int rankCount = System.Enum.GetValues(typeof(CharRank)).Length;
        for (int i = 0; i < rankCount; i++)
        {
            entityAsCodeDict = new Dictionary<int, Entity>();

            charListAsRank = charListAsRanks[i];

            codeList = new int[charListAsRank.Entities.Length];

            for (int j = 0; j < charListAsRank.Entities.Length; j++)
            {
                entity = charListAsRank.Entities[j];
                character = entity as Character;
                int code = character.Data.Code;
                entityAsCodeDict.Add(code, entity);

                codeList[j] = code;
            }
            charListAsRankDict.Add(charListAsRank.Rank, entityAsCodeDict);
            codeListDict.Add(charListAsRank.Rank, codeList);

            factory = factories[i];
            fc = factory as FactoryChar;
            factoryDict.Add(fc.Rank, factory);

            factory.Initialize(entityAsCodeDict);
        }
    }

    public void OnSpawnEntity()
    {
        CharRank rank = (CharRank)Random.Range(0, (int)CharRank.legendary + 1);

        int code = DetermineEntity(rank);
        Platform platform = SearchPlatform(code);

        factoryDict[rank].ActiveEntity(code, platform);
    }

    private int DetermineEntity(CharRank rank)
    {
        int rankCount = System.Enum.GetValues(typeof(CharRank)).Length;
        int rankIndex = Random.Range(0, rankCount); // 랭크별 확률에 따라 작동하도록 추후 수정
        
        int entityCount = charListAsRankDict[rank].Count;
        int index = Random.Range(0, codeListDict[rank].Length);
        int entityCode = codeListDict[rank][index];

        Character character = charListAsRankDict[rank][entityCode] as Character;
        return character.Data.Code;
    }

    private Platform SearchPlatform(int code)
    {
        List<Platform> availablePlatforms = new List<Platform>();
        
        foreach(var platform in platforms)
        {
            if(platform.CheckEntityAvailable(code))
            {
                availablePlatforms.Add(platform);
            }
        }

        int index = Random.Range(0, availablePlatforms.Count);
        
        return availablePlatforms[index];
    }
}
