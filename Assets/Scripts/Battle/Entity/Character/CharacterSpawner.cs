using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;
    [SerializeField] private CharListAsRank[] charListAsRanks;

    private Dictionary<CharRank, AbstractFactory> factoryDict;

    private Dictionary<int, Entity> entityAsCodeDict;

    private void Start()
    {
        factoryDict         = new Dictionary<CharRank, AbstractFactory>();

        Initialize();
    }

    public void Initialize()
    {
        AbstractFactory factory;
        FactoryChar fc;
        CharListAsRank charListAsRank;
        Entity entity;
        Character character;

        int rankCount = System.Enum.GetValues(typeof(CharRank)).Length;
        for (int i = 0; i < rankCount; i++)
        {
            entityAsCodeDict = new Dictionary<int, Entity>();

            charListAsRank = charListAsRanks[i];

            for (int j = 0; j < charListAsRank.Entities.Length; j++)
            {
                entity = charListAsRank.Entities[j];
                character = entity as Character;
                int code = character.Data.Code;
                entityAsCodeDict.Add(code, entity);
            }

            factory = factories[i];
            fc = factory as FactoryChar;
            factoryDict.Add(fc.Rank, factory);

            factory.Initialize(entityAsCodeDict);
        }
    }

    public void OnSpawnEntity()
    {
        CharRank rank = (CharRank)Random.Range(0, (int)CharRank.legendary + 1);

        factoryDict[rank].ActiveEntity();
    }
}
