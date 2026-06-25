using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Scriptable Objects/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
    [Tooltip("Seconds between each spawn attempt")]
    [SerializeField] private float spawnInterval = 1.5f;

    [Tooltip("Distance from center at which enemies spawn")]
    [SerializeField] private float spawnRadius = 20f;

    public float SpawnInterval => spawnInterval;
    public float SpawnRadius => spawnRadius;
}
