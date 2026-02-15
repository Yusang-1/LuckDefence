using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    

    public void OnGameStart()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
