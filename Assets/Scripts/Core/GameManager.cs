using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/*
rename all Vars and fields
*/

public class GameManager : MonoBehaviour
{
    private InputHandler _inputHandler = new();
    private CancellationTokenSource _cts;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CameraManagment _cameraManagment;

    [Header("State")]
    private bool _isRunning = false;

    [Header("Time")]
    [SerializeField] private float _startTime_BASE = 150f;
    [SerializeField] private float _currentTime;

    [Header("PreRun")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _spawnPos;

    public static Action StartRun;
    public static Action<float, float> HealthChange; // current, max
    public static Action<float> TimeUse;
    public static Action<float> TimeEarn;

    #region Debug
    [Header("debub")]
    [SerializeField] private bool _debug_IsPlaying = false;
    #endregion

    private void Awake()
    {
        _inputHandler.Init();

        if ( _debug_IsPlaying )
        {
            OnStartRun();
        }
    }

    private void Start()
    {
        _cameraManagment.ViewCamera.targetDisplay = 0;
    }

    private void OnEnable()
    {
        StartRun += OnStartRun;
        TimeEarn += OnTimeEarn;
        TimeUse += OnTimeUse;
    }

    private void OnDisable()
    {
        StartRun -= OnStartRun;
        TimeEarn -= OnTimeEarn;
        TimeUse -= OnTimeUse;
    }

    private void OnDestroy()
    {
        // ???
        _inputHandler.Inputs?.Disable();
        _inputHandler.RemoveInputs(_playerController);
        _inputHandler.Inputs?.Dispose();
        // ???
    }

    private void OnStartRun()
    {
        if (_isRunning) return;

        _isRunning = true;
        _cameraManagment.ViewCamera.targetDisplay = 1;

        _playerController = Instantiate(_playerController, _spawnPos.position, _spawnPos.rotation);

        _currentTime = _startTime_BASE;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _uiManager.SetActiveGameInterface(true);

        _inputHandler.InitInputs(_playerController);
        _inputHandler.Inputs.Enable();

        _cts = new();
        CountdownTask(_cts.Token).Forget();
    }

    private void OnTimeUse(float timeToUse)
    {
        _currentTime -= timeToUse;
    }

    private void OnTimeEarn(float timeToEarn)
    {
        _currentTime += timeToEarn;
    }

    private void FinishGame()
    {
        _isRunning = false;
        _cameraManagment.ViewCamera.targetDisplay = 0;

        _uiManager.SetActiveGameInterface(false);
    }

    private void Restart()
    {
        Reset();

        OnStartRun();
    }

    private void Reset()
    {
        _uiManager.SetActiveGameInterface(false);

        _isRunning = false;
        _currentTime = 0f;

        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTask CountdownTask(CancellationToken token)
    {
        while (_isRunning)
        {
            await UniTask.NextFrame();
            _currentTime -= Time.deltaTime;

            _uiManager.UpdateTimer(_currentTime);
        }
    }
}
