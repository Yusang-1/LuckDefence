using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Scriptable Objects/Player Resources")]
public class PlayerResourcesSO : ScriptableObject
{
    public event Action ResourcesChanged;

    [SerializeField] private int playerLevel;
    [SerializeField] private string playerName;
    [SerializeField] private int playerCoin;

    public int PlayerLevel => playerLevel;
    public string PlayerName => playerName;
    public int PlayerCoin => playerCoin;

    public void ChangePlayerLevel(int level)
    {
        playerLevel = level;
        ResourcesChanged?.Invoke();
    }

    public void ChangePlayerCoin(int coin) 
    {
        playerCoin = Mathf.Clamp(playerCoin+coin, 0, playerCoin + coin);
        ResourcesChanged?.Invoke();
    }

    public void ChangePlayerName(string name)
    {
        playerName = name;
        ResourcesChanged?.Invoke();
    }    
}
