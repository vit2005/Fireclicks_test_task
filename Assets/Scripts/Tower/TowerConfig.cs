using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Objects/TowerConfig")]
public class TowerConfig : ScriptableObject, IHealthDataProvider
{
    [SerializeField] private int MaxHealth;
    public int maxHealth => MaxHealth;
}
