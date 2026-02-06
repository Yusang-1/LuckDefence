using UnityEngine;

public class Promotion : MonoBehaviour
{
    //public event Action<bool> PromotionableChanged;
    
    [SerializeField] private Platforms platforms;
    [SerializeField] private CharacterSpawner spawner;

    private PlatformData platformData;
    private bool isPromotionable;

    public bool IsPromotionable
    {
        get => isPromotionable;
        set
        {
            isPromotionable = value;
            //PromotionableChanged?.Invoke(isPromotionable);
        }
    }

    public void GetPlatformData(PlatformData data, bool isPormotionable)
    {
        platformData = data;
        IsPromotionable = isPormotionable;
    }

    public void OnPromotion()
    {
        spawner.PromotionEntity(platformData);
        IsPromotionable = false;
    }
}
