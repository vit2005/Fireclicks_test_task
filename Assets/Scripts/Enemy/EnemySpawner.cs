using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EnemyPool
{
    public DefaultObjectPool pool;
    public int weight;
    public float timeToSpawnAvailable;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyPool> enemiesPools;
}
