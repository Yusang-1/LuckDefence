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

    private float currentTime;
    private bool isPlaying;
    private IEnumerator timeCoroutine;

    protected float CurrentTime
    {
        get => currentTime;
        set
        {
            currentTime = Mathf.Clamp(value, 0, maxTime);
            ChangeText(currentTime.ToString("N2"));

            if (currentTime == 0 && isPlaying)
            {
                TimeIsOver?.Invoke();
            }
        }
    }

    public void Initialize()
    {
        CurrentTime = 0;
        timeCoroutine = null;
    }

    public void OnStartTimerAddTime(RoundData data)
    {
        isPlaying = true;

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

    public void OnResetTimer()
    {
        isPlaying = false;

        if (timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
            timeCoroutine = null;
        }

        CurrentTime = 0;
    }
}
