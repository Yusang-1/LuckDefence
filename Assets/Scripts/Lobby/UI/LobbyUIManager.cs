using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private CharacterShopUI characterShopUI;    

    private void Start()
    {
        StartCoroutine(characterShopUI.Initialize());
    }
}
