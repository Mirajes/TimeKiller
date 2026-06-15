using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UI_Manager _uiManager;

    [Header("Stage")]
    [SerializeField] private StageManagement _stageManagement = new();
    private InputHandler _inputHandler = new();

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private int _adPoints = 0;

    // remake
    public static Action<float, float> HealthChange;
    public static Action<float> TimeEarn;
    public static Action<float> TimeSpent;
    public static Action StartRun;
    public static Action EndRun;
    //

    private void Awake()
    {
        _inputHandler.InitInputs(_player.Controller);

        Player.PlayerDied += OnPlayerDied;
    }

    private void OnDestroy()
    {
        Player.PlayerDied -= OnPlayerDied;
    }

    private void OnStartRun()
    {
        Transform spawnPos = _stageManagement.StarterStage.SpawnPoint;
        _player = Instantiate(_player, spawnPos.position, spawnPos.rotation);

        _inputHandler.Inputs.Player.Enable();
    }

    private void OnEndRun()
    {
        
    }

    private void OnPlayerDied() 
    {
        _inputHandler.Inputs.Player.Disable();
    }
}

public class Player : MonoBehaviour, IDamageable
{
    public PlayerController Controller => _playerController;

    [SerializeField] private PlayerController _playerController;
    //private Inventory _inventory

    private float _maxHealth = 100;
    private float _currentHealth;

    public static event Action PlayerDied;

    public void Init()
    {
        _currentHealth = _maxHealth;
    }

    public void HandleHeal(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth) 
            _currentHealth = _maxHealth;
    }

    public void HandleHit(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerDied?.Invoke();
    }
}

[System.Serializable]
public class StageManagement
{
    public A_Stage StarterStage => _starterStage;

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

    private async UniTask CountdownTask() { }
}
