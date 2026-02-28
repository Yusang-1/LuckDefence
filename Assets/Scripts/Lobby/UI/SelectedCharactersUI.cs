using UnityEngine;
using System.Collections;

public class SelectedCharactersUI : MonoBehaviour
{
    [SerializeField] private GameObject portrait;
    [SerializeField] private RectTransform myRect;    

    [SerializeField] private float padding;
    [SerializeField] private float spacing;
    [SerializeField] private int columnCount;

    private CharacterData characterData;
    private CharacterPortraitContainer[] portraits;

    private int selectedCount;    

    public IEnumerator Initialize(CharacterData characterData)
    {
        this.characterData = characterData;
        portraits = new CharacterPortraitContainer[characterData.AllCount];

        RectTransform portraitRect;
        
        float portraitWidth = (myRect.rect.width - ((padding * 2) + (spacing * (columnCount - 1)))) / columnCount;
        //Debug.Log($"{myRect.rect.width}, {portraitWidth}");
        int column = 0, row = 0;
        Vector2 pos;

        GameObject go;
        for(int i = 0; i < characterData.AllCount; i++)
        {
            go = Instantiate(portrait, myRect);
            portraits[i] = go.GetComponent<CharacterPortraitContainer>();
            portraitRect = portraits[i].GetComponent<RectTransform>();
            
            portraitRect.sizeDelta = new Vector3(portraitWidth, portraitWidth, 1);

            pos.x = padding + column * (portraitRect.sizeDelta.x + spacing) + portraitRect.sizeDelta.x / 2 - myRect.rect.width / 2;
            pos.y = -padding - row * (portraitRect.sizeDelta.y + spacing) - portraitRect.sizeDelta.y / 2 + myRect.rect.height / 2;
            
            portraitRect.localPosition = pos;
            //Debug.Log($"{column}, {columnCount - 1}, {column % (columnCount - 1)}");
            if (column != 0 && column % (columnCount-1) == 0)
            {
                column = 0;
                row++;
            }
            else
            {
                column++;
            }
            
            yield return null;
        }        
    }

    public void UpdateUI()
    {
        if(characterData.SelectedCharacterListData.IsDirty == false)
        {
            return;
        }

        selectedCount = 0;
        foreach (var charListAsRank in characterData.SelectedCharacterListData.CharListAsRankDictionary)
        {
            if(charListAsRank.Value.IsDirty == false)
            {
                selectedCount += characterData.RankCountByRank[charListAsRank.Key];
                continue;
            }

            foreach(var entityAsCode in charListAsRank.Value.EntityAsCodeDict)
            {
                portraits[selectedCount].SetPortrait(entityAsCode.Value);
            }
            charListAsRank.Value.SetDirty(false);
        }
        characterData.SelectedCharacterListData.IsDirty = false;
    }
}
