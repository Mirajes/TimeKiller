using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public abstract class A_Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _MaxHealth;
    [SerializeField] protected float _CurrentHealth;

    [SerializeField] protected float _TimeToDie;
    [SerializeField] protected float _CurrentTime;

    [SerializeField] protected NavMeshAgent _Agent;
    [SerializeField] private HealthBar _healthBar;
    protected CancellationTokenSource _aliveCTS;

    public event Action<A_Enemy> Die;

    [Header("DELETE")]
    [SerializeField] private bool _debug = false;

    private void Start()
    {
        if (_debug)
        {
            _CurrentHealth = _MaxHealth;
            _CurrentTime = _TimeToDie;
            //OnSpawn();
        }
    }

    public virtual void OnSpawn()
    {
        _CurrentHealth = _MaxHealth;
        _CurrentTime = _TimeToDie;

        _aliveCTS = new();

        CountdownToDieTask(_aliveCTS.Token).Forget();
    }

    public virtual void HandleHit(float damage)
    {
        print(damage);
        _CurrentHealth -= damage;
        _healthBar.ChangeValue(_CurrentHealth, _MaxHealth);

        if (_CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void HandleHeal(float amount)
    {
        throw new NotImplementedException();
    }

    protected virtual void Death()
    {
        _aliveCTS?.Cancel();
        _aliveCTS?.Dispose();

        if (_CurrentTime > 0)
        {
            GameManager.TimeEarn?.Invoke(_CurrentTime);
            _CurrentTime = 0;
        }

        Die?.Invoke(this);

        //gameObject.SetActive(false); // **** OBJ POOL
        Destroy(this.gameObject); // REMAKE ****
    }

    protected virtual async UniTask CountdownToDieTask(CancellationToken token)
    {
        while (_CurrentTime > 0)
        {
            token.ThrowIfCancellationRequested();

            await UniTask.NextFrame();
            _CurrentTime -= Time.deltaTime;
        }

        Death();
    }

    protected virtual void MakeSound()
    {

    }


}

//public class HitBox : MonoBehaviour
//{
//    [SerializeField] private Entity _parent;


//}

//public class Entity : MonoBehaviour { }