using UnityEngine;

public class OpenCharacterShopUI : MonoBehaviour
{
    [SerializeField] private CharacterShopUI characterShopUI;

    public void OnOpenCharacterShop()
    {
        characterShopUI.OpenUI();
    }
}
