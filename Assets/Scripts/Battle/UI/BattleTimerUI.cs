using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class BattleTimerUI : MonoBehaviour
{
    public event Action TimeIsOver;

    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float minTimeUnit;
    [SerializeField] private float warningTime;
    [SerializeField] private float maxTime;

    private float m_currentTime;

    private IEnumerator timeCoroutine;

    protected float CurrentTime
    {
        get => m_currentTime;
        set
        {
            m_currentTime = Mathf.Clamp(value, 0, maxTime);
            ChangeText(m_currentTime.ToString("N2"));

            if (m_currentTime == 0)
            {
                TimeIsOver?.Invoke();
            }
        }
    }

    public void OnStartTimerAddTime(RoundData data)
    {
        CurrentTime += data.AdditionalTime;

        if(timeCoroutine == null)
        {
            timeCoroutine = TimeUpdate();
            StartCoroutine(timeCoroutine);
        }
    }

    private IEnumerator TimeUpdate()
    {
        yield return null;
        float time = 0;

        while(true)
        {
            if(CurrentTime == 0)
            {
                yield return null;
                continue;
            }

            time += Time.deltaTime;

            if(time >= minTimeUnit)
            {
                time -= minTimeUnit;
                CurrentTime -= minTimeUnit;
            }

            yield return null;
        }
    }

    private void ChangeText(string text)
    {
        timerText.text = text;
    }
}
