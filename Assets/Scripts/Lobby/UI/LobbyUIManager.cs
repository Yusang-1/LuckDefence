using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private CharacterShopUI characterShopUI;

    private void Start()
    {
        characterShopUI.Initialize();
    }
}
