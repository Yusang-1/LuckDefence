using UnityEngine;

public class PlatformHoldSelector : MonoBehaviour
{
    private int holdedIndex;

    void Update()
    {
        //DrawLine();
    }

    public void Initialize()
    {

    }

    public void Holded(int platformIndex)
    {
        holdedIndex = platformIndex;
    }

    public void Released(int platformIndex)
    {
        if(holdedIndex != platformIndex)
        {

        }
    }
}
