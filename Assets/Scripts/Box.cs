using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar _healthBar;

    private float _maxHealth = 3;
    private float _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void HandleHit(float damage)
    {
        _currentHealth -= damage;
        _healthBar.ChangeValue(_currentHealth, _maxHealth);
    }
}
