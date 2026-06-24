using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    [SerializeField] private Health health;

    public Health Health => health;

    private void Awake()
    {
        health.Init(config);
    }
}
