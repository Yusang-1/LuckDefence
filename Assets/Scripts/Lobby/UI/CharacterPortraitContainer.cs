using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterPortraitContainer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TextMeshProUGUI characterName;
    
    private AbstractUI characterShopUI;
    private int characterCode;

    public int CharacterCode => characterCode;

    //public void Initialize(Entity entity)
    //{
    //    characterPortrait = entity.Data.Portrait;
    //    characterName.text = entity.Data.EntityName;
    //    this.entity = entity;
    //}

    public void Initialize(Entity entity, AbstractUI characterShopUI)
    {
        characterName.text = entity.Data.EntityName;
        characterCode = entity.Data.Code;
        this.characterShopUI = characterShopUI;

    }

    public void SetPortrait(Entity entity)
    {
        characterName.text = entity.Data.EntityName;
        characterCode = entity.Data.Code;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        characterShopUI.PortraitSelected(characterCode);
    }
}
