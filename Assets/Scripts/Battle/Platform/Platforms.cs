using System;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public event Action<Platform> PlatformSelected;

    [SerializeField] private Platform[] platformList;
    
    private int selectedPlatformIndex;

    public Platform[] PlatformList => platformList;    

    public int SelectedPlatformIndex
    {
        get => selectedPlatformIndex;
        set
        {
            selectedPlatformIndex = value;
            
            if(value >= 0 && platformList[value].EntityCount > 0)
            {                
                PlatformSelected?.Invoke(platformList[value]);
            }
        }
    }

    private void Start()
    {        
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].GetIndex(i);
        }
    }
}