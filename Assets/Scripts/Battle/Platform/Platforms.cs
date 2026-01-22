using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private Platform[] platformList;
       
    public Platform[] PlatformList => platformList;    

    private void Start()
    {        
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].GetIndex(i);
        }
    }
}