using UnityEngine;
using TMPro;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI charName;

    public void SetInfoUI(Entity entity)
    {
        charName.text = entity.Data.EntityName;
    }
}
