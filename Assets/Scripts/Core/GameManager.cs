using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private StageManagement _stageManagement = new();
    private InputHandler _inputHandler = new();

    // remake
    public static Action<float, float> HealthChange;
    public static Action<float> TimeEarn;
    public static Action<float> TimeUse;
    public static Action StartRun;
    //
}

[System.Serializable]
public class StageManagement
{
    [SerializeField] private Stage_Starter _starterStage;
    [SerializeField] private List<A_Stage> _stages;

    [SerializeField] private int _currentStage = 0;
}

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public class StateMachine
{

}

[System.Serializable]
public class Timer
{
    [SerializeField] private float _starterTime;
    [SerializeField] private float _currentTime;
}

/*
public class GameManager : MonoBehaviour
{
    private InputHandler _inputHandler = new();
    private CancellationTokenSource _cts;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CameraManagment _cameraManagment;

    [Header("Stages")]
    [SerializeField] private A_Stage _starterStage;
    [SerializeField] private List<A_Stage> _stagePrefabs = new();
    [SerializeField] private List<A_Stage> _runStages = new();
    [SerializeField] private int _stageCount = 3;
    private int _currentStage = 0;

    [Header("State")]
    private bool _isRunning = false;
    private bool _isWon = false;

    [Header("Time")]
    [SerializeField] private float _startTime_BASE = 150f;
    [SerializeField] private float _currentTime;

    [Header("PreRun")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _spawnPos;

    [Header("Player")]
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _damageSpeed = 3f;

    public static Action StartRun;
    public static Action EndRun;
    public static Action StageNext;
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

        if (_debug_IsPlaying)
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

        StageNext += OnStageNext;

        EndRun += FinishGame;
    }

    private void OnDisable()
    {
        StartRun -= OnStartRun;
        TimeEarn -= OnTimeEarn;
        TimeUse -= OnTimeUse;

        StageNext -= OnStageNext;

        EndRun -= FinishGame;
    }

    private void OnDestroy()
    {
        // ???
        _inputHandler.Inputs?.Disable();
        _inputHandler.RemoveInputs(_playerController);
        _inputHandler.Inputs?.Dispose();
        // ???
    }

    private void Reset()
    {
        _runStages.Clear();
        _currentStage = 0;

        _isWon = false;
        _uiManager.SetActiveGameInterface(false);

        _isRunning = false;
        _currentTime = 0f;

        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void OnStartRun()
    {
        if (_isRunning) return; // what reason?

        _isRunning = true;
        _cameraManagment.ViewCamera.targetDisplay = 1;

        _currentTime = _startTime_BASE;

        _uiManager.SetActiveGameInterface(true);

        _playerController = Instantiate(_playerController, _spawnPos.position, _spawnPos.rotation);

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

        if (_isWon)
        {

        }
        else
        {

        }
    }

    private void InitGame()
    {
        int i = 0;

        _runStages.Add(_starterStage);
        i++;

        while (i < _stageCount)
        {
            int randomIndex = UnityEngine.Random.Range(0, _stagePrefabs.Count);
            _runStages.Add(_stagePrefabs[randomIndex]);
            i++;
        }

        StageNext?.Invoke();
    }

    private void OnStageNext()
    {

    }

    private void Restart()
    {
        Reset();

        OnStartRun();
    }

    private async UniTask CountdownTask(CancellationToken token)
    {
        while (_isRunning)
        {
            await UniTask.NextFrame();
            _currentTime -= Time.deltaTime;

            if (_currentTime < 0)
            {
                _currentHealth -= Time.deltaTime * _damageSpeed;
                if (_currentHealth < 0)
                {
                    EndRun?.Invoke();
                    return;
                }
            }

            _uiManager.UpdateTimer(_currentTime);
        }
    }
}

*/