using System.Collections.Generic;
using UnityEngine;

public abstract class A_Stage : MonoBehaviour
{
    [SerializeField] protected List<Spawner> _enemySpawners;

    public void SpawnEnemies()
    {
        foreach (var spawner in _enemySpawners)
        {
            
        }
    }
}

public class Stage_LVL1 : A_Stage
{

}

public class Stage_LVL2 : A_Stage
{

}

public class Stage_BossLVL1 : A_Stage
{

}

public class Spawner : MonoBehaviour
{
    [SerializeField] private A_Enemy _enemyPrefab;
}