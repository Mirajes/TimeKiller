using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("debub")]
    [SerializeField] private bool _START_RUN = true;

    [Header("Links")]
    [SerializeField] private UIManager _uiManager;

    [Header("Stage")]
    [SerializeField] private StageManagement _stageManagement = new();
    private InputHandler _inputHandler = new();

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private int _adPoints = 0;

    [Header("Timer")]
    [SerializeField] private Timer _timer;

    // remake
    public static Action<float, float> HealthChange;
    public static Action<float> TimeEarn;
    public static Action<float> TimeSpent;
    public static Action StartRun;
    public static Action EndRun;
    //

    public static Action<bool> Pause;

    private void Awake()
    {
        _inputHandler.Init();
        _inputHandler.InitInputs(_uiManager);
        _inputHandler.Inputs.UI.Enable();

        Player.PlayerDied += OnPlayerDied;
        Pause += OnPaused;

        if (_START_RUN)
            OnStartRun();
    }

    private void OnDestroy()
    {
        Player.PlayerDied -= OnPlayerDied;
        Pause -= OnPaused;

        _inputHandler.Inputs?.Disable();
        _inputHandler.Inputs?.Dispose();
        _inputHandler.RemoveInputs(_uiManager);
    }

    private void OnStartRun()
    {
        Transform spawnPos = _stageManagement.StarterStage.SpawnPoint;
        _player = Instantiate(_player, spawnPos.position, spawnPos.rotation);

        _player.Init();
        _uiManager.Init(_player);

        _inputHandler.InitInputs(_player.Controller);
        _inputHandler.Inputs.Player.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEndRun()
    {
        
    }

    private void OnPlayerDied() 
    {
        _inputHandler.Inputs.Player.Disable();
    }

    private void OnPaused(bool isPaused)
    {
        if (isPaused)
        {
            _inputHandler.Inputs.Player.Disable();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _inputHandler.Inputs.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

public class StateMachine
{

}
