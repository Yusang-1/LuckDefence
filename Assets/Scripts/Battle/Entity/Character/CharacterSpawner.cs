using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;
    [SerializeField] private Platform[] platforms;
    [SerializeField] private CharListAsRank[] charListAsRanks;

    private Dictionary<CharRank, AbstractFactory> factoryDict;
    private Dictionary<CharRank, CharListAsRank> charListAsRankDict;

    private void Start()
    {
        factoryDict         = new Dictionary<CharRank, AbstractFactory>();
        charListAsRankDict  = new Dictionary<CharRank, CharListAsRank>();

        Initialize();
    }

    public void Initialize()
    {
        AbstractFactory factory;
        FactoryChar fc;
        CharListAsRank charListAsRank;

        int rankCount = System.Enum.GetValues(typeof(CharRank)).Length;
        for(int i = 0; i < rankCount; i++)
        {
            charListAsRank = charListAsRanks[i];
            charListAsRankDict.Add(charListAsRank.Rank, charListAsRank);

            factory = factories[i];
            fc = factory as FactoryChar;
            factoryDict.Add(fc.Rank, factory);

            factory.Initialize(charListAsRank.Entities);
        }
    }

    public void SpawnEntity()
    {
        CharRank rank = (CharRank)Random.Range(0, (int)CharRank.legendary);

        Entity entity = DetermineEntity(rank);
        Platform platform = SearchPlatform(entity);
        
        factoryDict[rank].ActiveEntity(entity, platform);
    }

    private Entity DetermineEntity(CharRank rank)
    {
        return new Entity(); //수정
    }

    private Platform SearchPlatform(Entity entity)
    {
        List<Platform> availablePlatforms = new List<Platform>();
        
        foreach(var platform in platforms)
        {
            if(platform.CheckEntityAvailable(entity))
            {
                availablePlatforms.Add(platform);
            }
        }

        int index = Random.Range(0, availablePlatforms.Count-1);
        
        return availablePlatforms[index];
    }
}
