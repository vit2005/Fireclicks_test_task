using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyPool
{
    public DefaultObjectPool pool;
    public EnemyConfig config;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnerConfig spawnerConfig;
    [SerializeField] private Tower tower;
    [SerializeField] private List<EnemyPool> enemiesPools;

    private readonly List<Enemy> _activeEnemies = new();
    private readonly Dictionary<Enemy, DefaultObjectPool> _enemyPoolMap = new();
    private readonly List<EnemyPool> _availableBuffer = new();

    private float _spawnTimer;
    private bool _isActive;
    private float _gameTime;

    public event Action OnEnemyKilled;

    public IReadOnlyList<Enemy> ActiveEnemies => _activeEnemies;

    public void StartSpawning()
    {
        _isActive = true;
        _gameTime = 0f;
        _spawnTimer = spawnerConfig.SpawnInterval;
    }

    public void StopSpawning()
    {
        _isActive = false;
    }

    private void Update()
    {
        if (!_isActive) return;

        _gameTime += Time.deltaTime;
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            _spawnTimer = spawnerConfig.SpawnInterval;
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        _availableBuffer.Clear();
        int totalWeight = 0;

        foreach (var entry in enemiesPools)
        {
            if (_gameTime >= entry.config.TimeToSpawnAvailable)
            {
                _availableBuffer.Add(entry);
                totalWeight += entry.config.Weight;
            }
        }

        if (_availableBuffer.Count == 0 || totalWeight == 0) return;

        SpawnEnemy(PickWeightedRandom(_availableBuffer, totalWeight));
    }

    private static EnemyPool PickWeightedRandom(List<EnemyPool> available, int totalWeight)
    {
        int roll = UnityEngine.Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (var entry in available)
        {
            cumulative += entry.config.Weight;
            if (roll < cumulative)
                return entry;
        }

        return available[available.Count - 1];
    }

    private void SpawnEnemy(EnemyPool entry)
    {
        GameObject obj = entry.pool.GetInstance();
        if (!obj.TryGetComponent<Enemy>(out var enemy)) return;

        obj.transform.position = RandomSpawnPosition();
        enemy.Init(entry.config, tower);
        RegisterEnemy(enemy, entry.pool);
    }

    private Vector3 RandomSpawnPosition()
    {
        Vector2 circle = UnityEngine.Random.insideUnitCircle.normalized;
        return new Vector3(circle.x, 0f, circle.y) * spawnerConfig.SpawnRadius;
    }

    private void RegisterEnemy(Enemy enemy, DefaultObjectPool sourcePool)
    {
        _activeEnemies.Add(enemy);
        _enemyPoolMap[enemy] = sourcePool;
        enemy.OnDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnEnemyDeath;
        _activeEnemies.Remove(enemy);

        if (_enemyPoolMap.TryGetValue(enemy, out var pool))
        {
            _enemyPoolMap.Remove(enemy);
            enemy.ResetState();
            pool.ReleaseInstance(enemy.gameObject);
            OnEnemyKilled?.Invoke();
        }
    }
}
