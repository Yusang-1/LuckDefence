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

    private bool isInSelectedArea;

    public int CharacterCode => characterCode;

    //public void Initialize(Entity entity)
    //{
    //    characterPortrait = entity.Data.Portrait;
    //    characterName.text = entity.Data.EntityName;
    //    this.entity = entity;
    //}

    public void Initialize(Entity entity, AbstractUI characterShopUI, bool isInSelectedArea)
    {
        if(entity != null)
        {
            characterName.text = entity.Data.EntityName;
            characterCode = entity.Data.Code;
        }
        this.characterShopUI = characterShopUI;
        this.isInSelectedArea = isInSelectedArea;
    }

    public void SetPortrait(Entity entity)
    {
        if(entity == null)
        {
            characterName.text = "";
            characterCode = 0;
            return;
        }

        characterName.text = entity.Data.EntityName;
        characterCode = entity.Data.Code;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isInSelectedArea == true)
        {
            characterShopUI.RemovePortrait(characterCode);
        }
        else
        {
            characterShopUI.PortraitSelected(characterCode);
        }
    }
}
