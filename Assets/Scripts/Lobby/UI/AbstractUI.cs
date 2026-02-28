using System.Collections;
using UnityEngine;

public abstract class AbstractUI : MonoBehaviour
{
    public void ActiveUI()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveUI()
    {
        gameObject.SetActive(false);
    }

    public abstract IEnumerator Initialize();

    public abstract void PortraitSelected(int code);
}
