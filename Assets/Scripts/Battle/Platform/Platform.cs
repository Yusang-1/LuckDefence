using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private int currentEntityCode; //코드로 바꾸기
    private int entityCount;

    private const int maxAvailableEntityCount = 3;
    private GameObject[] entities;

    public void Start()
    {
        entities = new GameObject[maxAvailableEntityCount];
        platformPosData.Initialize();
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

    public bool CheckEntityAvailable(int code)
    {
        return (entityCount == 0 || (currentEntityCode == code && entityCount < maxAvailableEntityCount));
    }

    public void EntitySpawned(GameObject spawnedObject)
    {
        Character ch = spawnedObject.GetComponent<Entity>() as Character;
        if(ch.Data.Rank >= CharRank.legendary)
        {
            entityCount = maxAvailableEntityCount;
        }
        else
            entityCount++;

        currentEntityCode = ch.Data.Code;
        entities[entityCount] = spawnedObject;
    }
}
