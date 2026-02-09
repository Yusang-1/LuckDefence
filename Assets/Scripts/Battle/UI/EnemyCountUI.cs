using UnityEngine;
using TMPro;

public class EnemyCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCountText;        

    public void OnChangeText(int count)
    {
        enemyCountText.text = $"{count.ToString()} /";
    }
}
