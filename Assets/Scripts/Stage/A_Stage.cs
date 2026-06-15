using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class A_Stage : MonoBehaviour
{
    public Transform SpawnPoint => _SpawnPoint;

    [SerializeField] protected Transform _SpawnPoint;
    [SerializeField] protected List<Spawner> _EnemySpawners;
    [SerializeField] protected List<Transform> _PortalPoints;
    
    [SerializeField] private List<A_Enemy> _enemies = new();
    [SerializeField] protected Portal _Portal;

    private float _spawnDelay = 0.2f;
    private bool _isStageCompleted = false;

    private int _enemyCount = 0;

    public virtual async UniTask SpawnEnemies(CancellationToken token)
    {
        _enemyCount = 0;

        // TODO: Make Obj Pool
        foreach (var spawner in _EnemySpawners)
        {
            _enemyCount++;

            A_Enemy enemy = Instantiate(spawner.Enemy, spawner.transform.position, spawner.transform.rotation);

            enemy.OnSpawn();
            _enemies.Add(enemy);
            enemy.Die += OnEnemyDie;

            await UniTask.Delay(System.TimeSpan.FromSeconds(_spawnDelay), cancellationToken: token);
        }
    }

    public async UniTask AwaitForCompletionTask(CancellationToken token)
    {
        await UniTask.WaitUntil(() => _enemyCount == 0);
        _isStageCompleted = true;

        SpawnPortal();
    }

    private void OnEnemyDie(A_Enemy enemy)
    {
        _enemyCount--;
        enemy.Die -= OnEnemyDie;
        _enemies.Remove(enemy);
    }

    private void SpawnPortal()
    {
        int randomIndex = Random.Range(0, _PortalPoints.Count);
        Instantiate<Portal>(_Portal, _PortalPoints[randomIndex]);
    }
}
