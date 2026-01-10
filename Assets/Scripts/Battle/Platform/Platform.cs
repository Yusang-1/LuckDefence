using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private Entity currentEntity; //코드로 바꾸기
    private int entityCount;

    private const int maxAvailableEntityCount = 3;
    private Entity[] entities;

    public void Start()
    {
        entities = new Entity[maxAvailableEntityCount];
    }

    public Vector3 GetPosition(CharRank rank)
    {
        Vector3 pos = platformPosData.PlatformPosDict[rank].Pos[entityCount] + transform.position;

        return pos;
    }

    public void ResetPlatform()
    {
        entityCount = 0;
    }

    public bool CheckEntityAvailable(Entity entity)
    {
        return (entityCount == 0 || (currentEntity == entity && entityCount < maxAvailableEntityCount));
    }

    public void EntitySpawned(Entity entity)
    {
        Character ch = entity as Character;
        if(ch.Data.Rank >= CharRank.legendary)
        {
            entityCount = maxAvailableEntityCount;
        }
        else
            entityCount++;

        entities[entityCount] = entity;
    }
}
