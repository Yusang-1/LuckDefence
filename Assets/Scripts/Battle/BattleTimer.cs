using UnityEngine;
using System.Collections;
using TMPro;

public class BattleTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    private float time;
    private IEnumerator timeCoroutine;

    public void StartNextRound(int additionalTime)
    {
        time = additionalTime;

        if(timeCoroutine == null)
        {
            timeCoroutine = TimeIsTicking();
            StartCoroutine(timeCoroutine);
        }
    }

    private IEnumerator TimeIsTicking()
    {
        timer.richText = true;
        WaitForSeconds waitOneSecond = new WaitForSeconds(0.01f);
        while(true)
        {
            yield return waitOneSecond;

            time--;

            if(time <= 10)
            {
                timer.text = $"<color=red>{time.ToString()}";
            }
            else
            {
                timer.text = time.ToString();
            }
        }
    }
}
