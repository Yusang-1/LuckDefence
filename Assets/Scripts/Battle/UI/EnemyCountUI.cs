using UnityEngine;
using TMPro;

public class EnemyCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI enemyLimitCountText;

    int limitCount;

    public void Initialize(int limitCount)
    {
        this.limitCount = limitCount;
        enemyCountText.text = "0";
        enemyLimitCountText.text = limitCount.ToString();
    }

    public void OnChangeText(int count)
    {
        enemyCountText.text = count.ToString();
    }

    public void OnReset()
    {
        enemyCountText.text = "0";
        enemyLimitCountText.text = limitCount.ToString();
    }
}
