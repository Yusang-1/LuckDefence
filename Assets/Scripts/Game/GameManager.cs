using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {
        uiManager.ChangeMainUI();
    }
}
