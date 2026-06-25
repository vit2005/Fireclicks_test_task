using System.Collections.Generic;
using UnityEngine;

public class FireballImplementation : SpellImplementation
{
    [SerializeField] private FireballConfig fireballConfig;
    [SerializeField] private FireballDirectionSectors directionSectors;
    [SerializeField] private DefaultObjectPool fireballPool;

    public float RuntimeAoeRadius { get; set; }

    private void Awake() => RuntimeAoeRadius = fireballConfig.AoeRadius;

    public override void Cast(Transform origin, RuntimeSpellStats stats, IReadOnlyList<Enemy> targets)
    {
        if (targets.Count == 0) return;

        Vector3 direction = directionSectors.GetBestDirection();
        if (direction == Vector3.zero) return;

        GameObject obj = fireballPool.GetInstance();
        if (obj.TryGetComponent<Fireball>(out var fireball))
            fireball.Init(origin.position, direction, stats.damage, stats.projectileSpeed, fireballConfig, fireballPool, RuntimeAoeRadius);
    }
}
