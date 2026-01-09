using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformPositionSO platformPosData;
    [SerializeField] private Entity currentEntity;
    private int entityCount;

    public Vector3 GetPosition(CharRank rank)
    {
        //if (entityCount >= platformPosData.PlatformPosDict[rank].RoomArea) return null;

        Vector3 pos = platformPosData.PlatformPosDict[rank].Pos[entityCount] + transform.position;
        entityCount++;

        return pos;
    }

    public void ResetPlatform()
    {
        entityCount = 0;
    }

    public bool CheckEntityAvailable(Entity entity)
    {
        return (entityCount == 0 || currentEntity == entity);
    }
}
