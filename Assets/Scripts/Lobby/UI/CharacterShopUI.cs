using UnityEngine;
using System.Collections;

public class CharacterShopUI : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    [SerializeField] private OwnedCharListUI[] ownedCharListUIs;
    [SerializeField] private SelectedCharacterInfoUI selectedCharacterInfoUI;
    [SerializeField] private SelectedCharactersUI selectedCharactersUI;
    [SerializeField] private CharacterInfoUI characterInfoUI;

    private int selectedCharCode;
    private Entity selectedEntity;

    public int SelectedCharCode
    {
        get => selectedCharCode;
        set
        {
            selectedCharCode = value;
            selectedEntity = characterData.CharacterListData.CharListAsRankDictionary[characterData.GetCharRankByCode(selectedCharCode)].EntityAsCodeDict[selectedCharCode];
            characterInfoUI.SetInfoUI(selectedEntity);
        }
    }

    public IEnumerator Initialize()
    {
        gameObject.SetActive(true);
        
        yield return StartCoroutine(selectedCharactersUI.Initialize(characterData));

        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].Initialize(characterData.CharacterListData.CharListAsRankDictionary[(CharRank)i], this);
        }

        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].OpenAllCharacterListUI();
        }
    }

    public void PortraitSelected(int code)
    {
        SelectedCharCode = code;


    }

    public void OnRecruit()
    {
        characterData.AddSelectedCharacter(selectedEntity);

        //selectedCharactersUI.AddCharacter(selectedEntity);
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        selectedCharactersUI.UpdateUI();
    }
}
