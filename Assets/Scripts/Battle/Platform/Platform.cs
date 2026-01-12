using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private int currentEntityCode; //코드로 바꾸기
    [SerializeField] private int entityCount;

    private const int maxAvailableEntityCount = 3;
    private GameObject[] entities;

    public void Start()
    {
        entities = new GameObject[maxAvailableEntityCount];
        entityCount = 0;
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
        //if (entityCount == 0) return true;
        //else if(currentEntityCode == code)
        //{
        //    if (entityCount < maxAvailableEntityCount) return true;
        //    else return false;
        //}
        //return false;
        //Debug.Log($"{gameObject.name}\ncurrentCode : {currentEntityCode}\nspawnCode : {code}\nentityCount : {entityCount}\nmaxCount : {maxAvailableEntityCount}\n{(entityCount == 0 || (currentEntityCode == code && entityCount != maxAvailableEntityCount))}");
        return (entityCount == 0 || (currentEntityCode == code && entityCount != maxAvailableEntityCount));
    }

    public void EntitySpawned(GameObject spawnedObject)
    {
        Character ch = spawnedObject.GetComponent<Entity>() as Character;        

        currentEntityCode = ch.Data.Code;
        entities[entityCount] = spawnedObject;

        entityCount += ch.Data.Weight;
    }
}
