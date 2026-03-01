using UnityEngine;
using System.Collections;

public class ManagedCharacterUI : AbstractUI, ILobbyUIState
{
    [SerializeField] private CharacterData characterData;

    [SerializeField] private OwnedCharListUI[] ownedCharListUIs;
    [SerializeField] private SelectedCharactersUI selectedCharactersUI;
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

        yield return StartCoroutine(selectedCharactersUI.Initialize(characterData));

        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].Initialize(characterData, characterData.OwnedcharacterListData.CharListAsRankDictionary[(CharRank)i], this);
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

    public override void PortraitSelected(int code)
    {
        SelectedCharCode = code;


    }

    public void OnAddCharacterToBattleList()
    {
        characterData.AddSelectedCharacter(selectedEntity);

        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].UpdateUI();
        }

        selectedCharactersUI.UpdateUI();
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
