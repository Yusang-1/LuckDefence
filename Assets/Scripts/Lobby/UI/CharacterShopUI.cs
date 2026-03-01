using UnityEngine;
using System.Collections;

public class CharacterShopUI : AbstractUI, ILobbyUIState
{
    [SerializeField] private CharacterData characterData;

    [SerializeField] private AllCharListUI[] allCharListUIs;
    //[SerializeField] private SelectedCharactersUI selectedCharactersUI;
    [SerializeField] private ManagedCharacterUI managedCharListUIs;
    [SerializeField] private CharacterInfoUI selectedCharacterInfoUI;

    private int selectedCharCode;
    private Entity selectedEntity;

    public int SelectedCharCode
    {
        get => selectedCharCode;
        set
        {
            selectedCharCode = value;
            selectedEntity = characterData.CharacterListData.CharListAsRankDictionary[characterData.GetCharRankByCode(selectedCharCode)].EntityAsCodeDict[selectedCharCode];
            selectedCharacterInfoUI.SetInfoUI(selectedEntity);
        }
    }

    public override IEnumerator Initialize()
    {
        gameObject.SetActive(true);

        yield return null;
        //yield return StartCoroutine(selectedCharactersUI.Initialize(characterData));

        for (int i = 0; i < allCharListUIs.Length; i++)
        {
            allCharListUIs[i].Initialize(characterData.CharacterListData.CharListAsRankDictionary[(CharRank)i], this);
        }

        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < allCharListUIs.Length; i++)
        {
            allCharListUIs[i].OpenAllCharacterListUI();
        }
    }

    public override void PortraitSelected(int code)
    {
        SelectedCharCode = code;


    }

    public void OnBuyCharacter()
    {
        characterData.AddOwnedCharacter(selectedEntity);

        //selectedCharactersUI.AddCharacter(selectedEntity);
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        managedCharListUIs.UpdateShopUI();
    }

    public void ActiveUI()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveUI()
    {
        gameObject.SetActive(false);
    }
}
