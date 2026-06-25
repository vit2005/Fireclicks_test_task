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
        if (targets.Count == 0)
        {
            Debug.Log("[Fireball] Cast skipped: no targets");
            return;
        }

        Vector3 direction = directionSectors.GetBestDirection(origin.position, debug: true);
        if (direction == Vector3.zero)
        {
            Debug.Log("[Fireball] Cast skipped: all sectors empty (no enemies inside any sector collider)");
            return;
        }

        // chosen direction — thick magenta ray for 5s
        Debug.DrawRay(origin.position, direction * 8f, Color.magenta, 5f);
        Debug.Log($"[Fireball] Casting toward {direction:F2}, targets={targets.Count}, damage={stats.damage}");

        GameObject obj = fireballPool.GetInstance();
        if (obj.TryGetComponent<Fireball>(out var fireball))
            fireball.Init(origin.position, direction, stats.damage, stats.projectileSpeed, fireballConfig, fireballPool, RuntimeAoeRadius);
        else
            Debug.LogError("[Fireball] Pool returned object without Fireball component");
    }
}
