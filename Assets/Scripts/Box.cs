using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar _healthBar;

    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private float _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void HandleHit(float damage)
    {
        _currentHealth -= damage;
        _healthBar.ChangeValue(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            DestroyBox();
        }
    }

    private void DestroyBox()
    {
        Destroy(this.gameObject);
    }

    public void HandleHeal(float amount)
    {
        throw new System.NotImplementedException();
    }
}
