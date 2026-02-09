using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    public void StartBattle()
    {
        stageManager.Initialize();
    }
    
    private void CheckEnemyNum()
    {

    }

    private void GameOver()
    {

    }
}