using UnityEngine;
using UnityEngine.AI;

public abstract class A_Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _MaxHealth;
    [SerializeField] protected float _CurrentHealth;

    [SerializeField] protected NavMeshAgent _agent;

    private void Start()
    {
        _CurrentHealth += _MaxHealth;
    }

    public virtual void HandleHit(float damage)
    {
        _CurrentHealth -= damage;

        if (_CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        gameObject.SetActive(false);
    }
}

//public class HitBox : MonoBehaviour
//{
//    [SerializeField] private Entity _parent;


//}

//public class Entity : MonoBehaviour { }