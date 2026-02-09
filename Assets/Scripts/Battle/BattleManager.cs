using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    private void StartBattle()
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