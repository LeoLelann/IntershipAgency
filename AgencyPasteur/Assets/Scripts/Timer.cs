using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTimeLow;
    private float _timerMax; 
    private float _currentTimer;
    [SerializeField] TMP_Text _UITimer;
     void Start()
    {
        _timerMax = GameManager.Instance.Timer1;
        _currentTimer = GameManager.Instance.Timer1;
        StartCoroutine(TimerVisualUpdate());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    IEnumerator  TimerVisualUpdate()
    {
        int time = (int)_currentTimer;
        while (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
            time = (int)_currentTimer;
            _UITimer.text = ($"{time}");
            if (time == 10)
            {
                _onTimeLow.Invoke();
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
