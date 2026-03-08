using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] CharacterData characterData;    

    public void OnGameStart()
    {
        if(characterData.isSelectedCharacterFull() == false)
        {
            return;
        }

        SceneChanger.LoadSceneAsync("BattleScene");
    }
}
