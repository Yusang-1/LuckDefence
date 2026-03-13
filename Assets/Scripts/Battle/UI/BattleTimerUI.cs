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

    private float tempTime;
    private int timeCount;
    private void Update()
    {
        if (CurrentTime == 0 || isPlaying == false)
        {
            return;
        }

        tempTime += Time.deltaTime;

        if (tempTime >= minTimeUnit)
        {
            timeCount = 0;
            while(tempTime > minTimeUnit)
            {
                tempTime -= minTimeUnit;
                timeCount++;
            }
            
            CurrentTime -= minTimeUnit * timeCount;
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
