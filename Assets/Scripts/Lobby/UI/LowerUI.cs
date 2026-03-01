using UnityEngine;
using System.Collections;

public class LowerUI : MonoBehaviour, ILobbyUIState
{   
    public IEnumerator Initialize()
    {
        yield return null;
    }

    public void ActiveUI()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveUI()
    {
        gameObject.SetActive(false);
    }
}
