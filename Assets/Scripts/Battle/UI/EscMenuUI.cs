using System;
using UnityEngine;

public class EscMenuUI : MonoBehaviour
{
    public event Action RetryStage;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void OnGoToMainMenu()
    {
        gameObject.SetActive(false);

        SceneChanger.LoadSceneAsync("LobbyScene");
    }

    public void OnRetryStage()
    {
        gameObject.SetActive(false);

        RetryStage?.Invoke();
    }

    // 버튼에 할당
    public void OnOpenEscMenu()
    {
        gameObject.SetActive(true);
    }

    // 버튼에 할당
    public void OnCloseEscMenu()
    {
        gameObject.SetActive(false);
    }
}
