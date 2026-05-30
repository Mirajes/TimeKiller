using UnityEngine;

public class Spawner : MonoBehaviour // ?
{
    public A_Enemy Enemy => _enemyPrefab;
    [SerializeField] private A_Enemy _enemyPrefab;
}