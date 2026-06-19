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

    public static Action<int> GetAdPoint;
    public static Action<int> RemoveAdPoint;
    public static Action<SO_Item> BuyItem;

    private void Awake()
    {
        _inputHandler.Init();
        _inputHandler.InitInputs(_uiManager);
        _inputHandler.Inputs.UI.Enable();

        Player.PlayerDied += OnPlayerDied;
        Pause += OnPaused;

        GetAdPoint += OnGetAdPoint;
        RemoveAdPoint += OnRemoveAdPoint;
        BuyItem += OnBuyItem;

        if (_START_RUN)
            OnStartRun();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("game");
    }

    private void OnDestroy()
    {
        Player.PlayerDied -= OnPlayerDied;
        Pause -= OnPaused;

        GetAdPoint -= OnGetAdPoint;
        RemoveAdPoint -= OnRemoveAdPoint;
        BuyItem -= OnBuyItem;

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

    private void OnGetAdPoint(int amount)
    {
        _adPoints += amount;
        UIManager.AdPointChange?.Invoke(_adPoints);
    }

    private void OnRemoveAdPoint(int amount)
    {
        _adPoints -= amount;
        UIManager.AdPointChange?.Invoke(_adPoints);
    }

    private void OnBuyItem(SO_Item item)
    {
        if (_adPoints >= item.AdCost)
        {
            RemoveAdPoint?.Invoke(item.AdCost);

            _uiManager.BuyItem(item);
            UIManager.Notify?.Invoke($"U bought - {item.Name}", NotificationType.Buy);
        }
        else
        {
            UIManager.Notify?.Invoke($"u haven't enough, cost is [{item.AdCost}]", NotificationType.Warn);
        }
    }
}

public class StateMachine
{

}
