using UnityEngine;

public class Platform : MonoBehaviour, ISelectableObject
{
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private int currentEntityCode;
    [SerializeField] private int entityCount;
    [SerializeField] private CharRank rank;

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
        Entity entity = spawnedObject.GetComponent<Entity>();        

        currentEntityCode = entity.Data.Code;
        entities[entityCount] = spawnedObject;

        CharacterSO charSO = entity.Data as CharacterSO;
        entityCount += charSO.Weight;

        rank = charSO.Rank;
    }

    public void Selected()
    {
        Debug.Log($"Selected : {name}");
        CheckIsPromotionable();
    }

    private void CheckIsPromotionable()
    {
        if(entityCount == maxAvailableEntityCount && rank < CharRank.legendary)
        {

        }
    }
}
