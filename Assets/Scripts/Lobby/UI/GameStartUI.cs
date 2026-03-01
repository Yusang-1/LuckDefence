using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    public void OnGameStart()
    {
        if(characterData.isSelectedCharacterFull() == false)
        {
            return;
        }

        SceneManager.LoadScene("BattleScene");
    }
}
