using System;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerField;

    //List<UI_TimePopUp> _timePopUps; // buffer => ??

    [SerializeField] private RectTransform _popUpRect;
    [SerializeField] private UI_TimePopUp _popUpPrefab;

    private void OnEnable()
    {
        GameManager.TimeEarn += OnTimeEarn;
        GameManager.TimeSpent += OnTimeSpent;
    }

    private void OnDisable()
    {
        GameManager.TimeEarn -= OnTimeEarn;
        GameManager.TimeSpent -= OnTimeSpent;
    }

    public void UpdateTimer(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        if (time > 0)
            _timerField.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        else
            _timerField.text = $"-{-timeSpan.Minutes}:{-timeSpan.Seconds:D2}";
    }

    private void OnTimeEarn(float timeAmount)
    {
        var newPopUp = Instantiate(_popUpPrefab, _popUpRect);
        newPopUp.Init(timeAmount, true);
    }

    private void OnTimeSpent(float timeAmount)
    {
        var newPopUp = Instantiate(_popUpPrefab, _popUpRect);
        newPopUp.Init(timeAmount, false);
    }
}