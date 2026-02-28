using UnityEngine;

public class OpenManageCharacterUI : MonoBehaviour
{
    [SerializeField] private ManagedCharacterUI managedCharacterUI;

    public void OnOpenManageCharacter()
    {
        managedCharacterUI.OpenUI();
    }
}
