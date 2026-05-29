using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton_fromRun;
    [SerializeField] private RectTransform _pauseWindow;
    [SerializeField] private RectTransform _settingsWindow;
    [SerializeField] private Image _crosshair;
    [SerializeField] private UI_HealthBar _healthBar;
    [SerializeField] private UI_TimePopUp _timePopUpPrefab;
    [SerializeField] private RectTransform _timePopUpSpawn;
    [SerializeField] private TMP_Text _timer;

    [Header("Menu")]
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _toCharacterListButton;
    [SerializeField] private Button _settingsButtonMenu;
    [SerializeField] private Button _creditButton;
    [SerializeField] private Button _exitButton_fromGame;
    [SerializeField] private RectTransform _menuWindow;
    [SerializeField] private RectTransform _characterWindow;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(StartRun);

        GameManager.HealthChange += OnHealthChange;
        GameManager.TimeEarn += OnTimeEarn;
        GameManager.TimeUse += OnTimeUse;
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();

        GameManager.HealthChange -= OnHealthChange;
        GameManager.TimeEarn -= OnTimeEarn;
        GameManager.TimeUse -= OnTimeUse;
    }

    private void StartRun()
    {
        GameManager.StartRun?.Invoke();

        //_gameCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        
    }

    private void OnHealthChange(float current, float max)
    {
        _healthBar.ChangeValue(current, max);
    }

    private void OnTimeEarn(float time)
    {
        var newPopUp = Instantiate(_timePopUpPrefab, _timePopUpSpawn);
        newPopUp.Init(time);
    }

    private void OnTimeUse(float time)
    {
        var newPopUp = Instantiate(_timePopUpPrefab, _timePopUpSpawn);
        newPopUp.Init(time);
    }

    public void UpdateTimer(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timer.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
    }

    public void SetActiveGameInterface(bool isEnabled)
    {
        _timer.transform.parent.gameObject.SetActive(isEnabled); // :)
        _healthBar.gameObject.SetActive(isEnabled);
    }
}
