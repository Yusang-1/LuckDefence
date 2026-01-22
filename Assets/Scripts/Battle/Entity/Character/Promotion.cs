using UnityEngine;
using System;

public class Promotion : MonoBehaviour
{
    public event Action<bool> PromotionableChanged;

    private Platform platform;
    [SerializeField] private CharacterSpawner spawner;

    private bool isPromotionable;

    public bool IsPromotionable
    {
        get => isPromotionable;
        set
        {
            isPromotionable = value;
            PromotionableChanged?.Invoke(isPromotionable);
        }
    }

    public void GetPlatform(Platform platform)
    {
        this.platform = platform;
    }

    public void OnPromotion()
    {
        spawner.PromotionEntity();
    }
}
