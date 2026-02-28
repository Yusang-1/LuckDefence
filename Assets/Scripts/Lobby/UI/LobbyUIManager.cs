using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LowerUI lowerUI;
    [SerializeField] private CharacterShopUI characterShopUI;
    [SerializeField] private ManagedCharacterUI managedCharacterUI;

    private List<AbstractUI> uis;

    private IEnumerator Start()
    {
        yield return StartCoroutine(characterShopUI.Initialize());

        yield return StartCoroutine(lowerUI.Initialize());

        yield return StartCoroutine(managedCharacterUI.Initialize());
    }
}
