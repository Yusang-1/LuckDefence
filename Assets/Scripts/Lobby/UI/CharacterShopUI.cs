using UnityEngine;

public class CharacterShopUI : MonoBehaviour
{
    [SerializeField] private OwnedCharListUI[] ownedCharListUIs;
    [SerializeField] private CharacterListDataSO characterListData;

    public void Initialize()
    {
        gameObject.SetActive(true);

        characterListData.Initialize();

        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].Initialize(characterListData.CharListAsRankDictionary[(CharRank)i]);
        }

        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        for (int i = 0; i < ownedCharListUIs.Length; i++)
        {
            ownedCharListUIs[i].OpenAllCharacterListUI();
        }
    }
}
