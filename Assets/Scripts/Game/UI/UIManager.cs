using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUI;
    [SerializeField] private BattleUIManager battleUI;
    [SerializeField] private LoadingUI loadingUI;
    [SerializeField] private GameObject currentActiveMainUI;

    [SerializeField] private GameObject[] MainUIListBySceneIndex;

    private void Start()
    {
        DontDestroyOnLoad(this);

        currentActiveMainUI = lobbyUI.gameObject; //임시
    }

    public void ChangeMainUI()
    {
        loadingUI.gameObject.SetActive(false);
        if(currentActiveMainUI != null)
        {
            currentActiveMainUI.SetActive(false);
        }

        currentActiveMainUI = MainUIListBySceneIndex[SceneManager.GetActiveScene().buildIndex];

        currentActiveMainUI.SetActive(true);
    }
}
