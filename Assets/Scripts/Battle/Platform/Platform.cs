using UnityEngine;

public class Platform : MonoBehaviour, ISelectableObject
{    
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private int currentEntityCode;
    [SerializeField] private int entityCount;
    [SerializeField] private CharRank rank;

    private const int maxAvailableEntityCount = 3;
    private GameObject[] entities;
    private int index;
    [SerializeField] private Promotion promotion;    

    public int Index => index;
    public CharRank Rank => rank;

    public void Start()
    {
        entities = new GameObject[maxAvailableEntityCount];
        entityCount = 0;
        platformPosData.Initialize();
        rank = CharRank.none;
    }

    public void GetIndex(int index)
    {
        this.index = index;
    }

    public Vector3 GetPosition(CharRank rank)
    {
        Vector3 pos = platformPosData.PlatformPosDict[rank].Pos[entityCount] + transform.position;

        return pos;
    }

    public void ResetPlatform()
    {
        entityCount = 0;
        currentEntityCode = 0;
        rank = CharRank.none;
        foreach (var entity in entities)
        {
            entity.SetActive(false);
        }
    }

    public bool CheckEntityAvailable(int code)
    {        
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
        
        bool value = CheckIsPromotionable();
        promotion.GetPlatformData(new PlatformData(index, rank), value);
    }

    public void SelectedEnd()
    {
        //IsPromotionable = false;
    }

    public void Holded()
    {
        Debug.Log($"Holded : {name}");
    }

    private bool CheckIsPromotionable()
    {
        if(entityCount == maxAvailableEntityCount && rank < CharRank.legendary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIsRankSummonable(CharRank rank)
    {
        if(this.rank == CharRank.none || this.rank == rank)
        {
            return true;
        }
        else
            return false;
    }
}

public struct PlatformData
{
    private int index;
    private CharRank rank;

    public int Index => index;
    public CharRank Rank => rank;

    public PlatformData(int index, CharRank rank)
    {
        this.index = index; this.rank = rank;
    }
}