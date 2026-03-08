using UnityEngine;
using System;

public class Platforms : MonoBehaviour
{
    public event Action<Platform> PlatformSelected;
    public event Action<Platform> PlatformDataChanged;

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
        selectedPlatformIndex = -1;

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].GetIndex(i);
        }
    }

    public void DataChanged(int index)
    {
        Debug.Log($"{index}  {selectedPlatformIndex}");
        if (index != selectedPlatformIndex) return;

        Debug.Log(PlatformDataChanged == null);
        PlatformDataChanged?.Invoke(platformList[index]);
    }
}