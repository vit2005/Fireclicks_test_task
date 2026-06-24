using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject, IHealthDataProvider
{
    [SerializeField] private int MaxHealth;
    public int maxHealth => MaxHealth;
}
