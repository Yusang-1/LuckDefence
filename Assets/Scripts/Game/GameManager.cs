using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public UIManager UIManager => uiManager;

    private static bool hasInstance = false;

    void Awake()
    {
        if (hasInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            hasInstance = true;

            DontDestroyOnLoad(gameObject);
        }
    }

    public void Initialize()
    {
        uiManager.ChangeMainUI();
    }
}
