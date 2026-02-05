using UnityEngine;
using UnityEngine.UIElements;

public class SelectPlatformUIContainer : MonoBehaviour
{
    [SerializeField] private SelectPlatformUI m_UIRight;
    [SerializeField] private SelectPlatformUI m_UILeft;

    [SerializeField] private Platforms platforms;

    public void Start()
    {
        platforms.PlatformSelected += OpenUI;

        m_UIRight.Initialize();
        m_UILeft.Initialize();
    }

    private void OnDestroy()
    {
        platforms.PlatformSelected -= OpenUI;
    }

    public void OpenUI(Platform platform)
    {
        if (Camera.main.WorldToScreenPoint(platform.transform.position).x >= Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)).x)
        {
            OpenLeftUI(platform);
        }
        else
        {
            OpenRIghtUI(platform);
        }
    }

    public void OpenRIghtUI(Platform platform)
    {
        if(m_UILeft.IsOpen)
        {
            m_UILeft.OnCloseUI();
        }

        m_UIRight.OpenUI(platform);
    }

    public void OpenLeftUI(Platform platform)
    {
        if (m_UIRight.IsOpen)
        {
            m_UIRight.OnCloseUI();
        }

        m_UILeft.OpenUI(platform);
    }
}
