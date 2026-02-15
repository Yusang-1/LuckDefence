using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPortraitContainer : MonoBehaviour
{
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TextMeshProUGUI characterName;

    public void Initialize(Image image, string name)
    {
        characterPortrait = image;
        characterName.text = name;
    }

    public void Initialize(string name)
    {
        characterName.text = name;
    }
}
