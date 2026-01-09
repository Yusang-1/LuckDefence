using UnityEngine;

public class FactoryChar : AbstractFactory
{
    [SerializeField] private CharRank rank;

    private GameObject[,] pooledCharacters;
    private int[] pooledCount;

    public CharRank Rank => rank;

    public override void Initialize(Entity[] entities)
    {
        base.Entities = entities;

        pooledCharacters = new GameObject[Entities.Length, pooledNum];
        pooledCount = new int[Entities.Length];

        GameObject go;
        for(int i = 0; i < Entities.Length; i++)
        {
            pooledCount[i] = 0;

            for(int j = 0; j < pooledNum; j++)
            {
                go = Instantiate(Entities[i].gameObject);
                go.SetActive(false);
                pooledCharacters[i, j] = go;
            }
        }
    }

    public override void ActiveEntity(Entity entity, Platform platform)
    {
        int index;
        do
        {
            index = Random.Range(0, Entities.Length - 1);
        } while (pooledCount[index] < pooledNum);        
        
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
