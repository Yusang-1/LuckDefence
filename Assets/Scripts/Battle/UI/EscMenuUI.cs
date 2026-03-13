using UnityEngine;

public class EscMenuUI : MonoBehaviour
{
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

    private void OnDestroy()
    {
        gameObject.SetActive(false);
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
