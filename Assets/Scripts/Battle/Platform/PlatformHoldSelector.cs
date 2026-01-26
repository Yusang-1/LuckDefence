using UnityEngine;

public class PlatformHoldSelector : MonoBehaviour
{
    [SerializeField] private Platforms platforms;

    private int holdedIndex;
    private int releasedIndex;

    void Update()
    {
        //DrawLine();
    }

    public void Initialize()
    {
        holdedIndex = -1;
        releasedIndex = -1;
    }

    public void Holded(int platformIndex)
    {
        holdedIndex = platformIndex;
    }

    public void Released(int platformIndex)
    {
        releasedIndex = platformIndex;

        DoJob();
    }

    private void DoJob()
    {
        if(holdedIndex == releasedIndex || holdedIndex == -1 || releasedIndex == -1)
        {
            return;
        }        

        GameObject[] entities = platforms.PlatformList[holdedIndex].Entities;
        foreach(GameObject go in entities)
        {
            if(go != null)
            {
                Entity entity = go.GetComponent<Entity>();

                entity.Mover.GetDestinationVector(platforms.PlatformList[releasedIndex].transform.position);
                entity.Mover.Move();

                platforms.PlatformList[releasedIndex].EntitySpawned(go);
            }            
        }

        platforms.PlatformList[holdedIndex].Migration();
    }
}
