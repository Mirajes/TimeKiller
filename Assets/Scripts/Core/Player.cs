using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerController Controller => _playerController;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    [SerializeField] private PlayerController _playerController;
    //private Inventory _inventory

    [SerializeField] private float _maxHealth = 100;
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

        AudioManager.Instance.PlaySFX("heal");
        GameManager.HealthChange?.Invoke(_currentHealth, _maxHealth); // ?
    }

    public void HandleHit(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }

        AudioManager.Instance.PlaySFX("hit");
        GameManager.HealthChange?.Invoke(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        PlayerDied?.Invoke();
    }
}
