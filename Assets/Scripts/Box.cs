using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private float _maxHealth = 3;
    private float _currentHealth;

    public void Hit(float damage)
    {
        _currentHealth -= damage;
    }
}
