using UnityEngine;

public class PooledEntity
{
    private GameObject[] pooledEntities;
    private int nowPooledCount;
    private int maxPooledCount;

    public PooledEntity(GameObject[] gameObjects, int poolNum)
    {
        pooledEntities = gameObjects;
        maxPooledCount = poolNum;
    }

    public GameObject GetLastActivatedEntity()
    {
        return pooledEntities[nowPooledCount - 1];
    }

    public bool ActiveEntity(Vector3 position)
    {
        pooledEntities[nowPooledCount].gameObject.SetActive(true);
        pooledEntities[nowPooledCount].transform.position = position;
        nowPooledCount++;

        if(nowPooledCount == maxPooledCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeactiveEntity()
    {

    }
}
