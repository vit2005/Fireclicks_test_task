using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject, IHealthDataProvider
{
    [SerializeField] private int maxHealthValue;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float scale = 1f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private int weight = 1;
    [SerializeField] private float timeToSpawnAvailable = 0f;

    public int maxHealth => maxHealthValue;
    public float Speed => speed;
    public int Damage => damage;
    public float AttackRate => attackRate;
    public float Scale => scale;
    public float StopDistance => stopDistance;
    public int Weight => weight;
    public float TimeToSpawnAvailable => timeToSpawnAvailable;
}
