using UnityEngine;

public class StartStageButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void OnStartStage()
    {
        FindFirstObjectByType<BattleManager>().StartBattle();

        gameObject.SetActive(false);
    }
}
