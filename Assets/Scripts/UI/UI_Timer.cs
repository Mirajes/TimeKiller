using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerField;

    List<UI_TimePopUp> _timePopUps; // buffer => ??

    [SerializeField] private UI_TimePopUp _popUpPrefab;
    [SerializeField] private RectTransform _popUpRect;

    public void UpdateTimer(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        if (time > 0)
            _timerField.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        else
            _timerField.text = $"-{-timeSpan.Minutes}:{-timeSpan.Seconds:D2}";
    }

    public void TimeEarn(float timeAmount)
    {
        _popUpPrefab.Init(timeAmount, true);
    }

    public void TimeSpent(float timeAmount)
    {
        _popUpPrefab.Init(timeAmount, false);
    }
}