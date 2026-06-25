using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Scriptable Objects/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
    [Tooltip("Spawn interval (seconds) over game time (seconds). X = game time, Y = interval between spawns.")]
    [SerializeField] private AnimationCurve spawnIntervalOverTime = new AnimationCurve(
        new Keyframe(0f,   2.0f),
        new Keyframe(60f,  1.2f),
        new Keyframe(180f, 0.6f),
        new Keyframe(360f, 0.25f)
    );

    [Tooltip("Distance from center at which enemies spawn")]
    [SerializeField] private float spawnRadius = 20f;

    public float GetSpawnInterval(float gameTime) => spawnIntervalOverTime.Evaluate(gameTime);
    public float SpawnRadius => spawnRadius;
}
