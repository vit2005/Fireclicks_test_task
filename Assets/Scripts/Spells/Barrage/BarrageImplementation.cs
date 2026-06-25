using System.Collections.Generic;
using UnityEngine;

public class BarrageImplementation : SpellImplementation
{
    [SerializeField] private BarrageConfig barrageConfig;
    [SerializeField] private DefaultObjectPool projectilePool;

    // -1 means use barrageConfig.MaxTargets; upgrades can override this
    public int RuntimeMaxTargets { get; set; } = -2;

    private readonly List<Enemy> _visibleBuffer = new();
    private Plane[] _frustumPlanes;

    private void Awake() => RuntimeMaxTargets = barrageConfig.MaxTargets;

    public override void Cast(Transform origin, RuntimeSpellStats stats, IReadOnlyList<Enemy> targets)
    {
        if (targets.Count == 0) return;

        BuildVisibleList(targets);
        if (_visibleBuffer.Count == 0) return;

        int count = RuntimeMaxTargets < 0
            ? _visibleBuffer.Count
            : Mathf.Min(RuntimeMaxTargets, _visibleBuffer.Count);

        for (int i = 0; i < count; i++)
            SpawnProjectile(origin.position, _visibleBuffer[i], stats);
    }

    private void BuildVisibleList(IReadOnlyList<Enemy> targets)
    {
        _visibleBuffer.Clear();
        _frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (var enemy in targets)
        {
            if (enemy == null) continue;

            Bounds bounds = enemy.TryGetComponent<Collider>(out var col)
                ? col.bounds
                : new Bounds(enemy.transform.position, Vector3.one);

            if (GeometryUtility.TestPlanesAABB(_frustumPlanes, bounds))
                _visibleBuffer.Add(enemy);
        }
    }

    private void SpawnProjectile(Vector3 origin, Enemy target, RuntimeSpellStats stats)
    {
        GameObject obj = projectilePool.GetInstance();
        if (obj.TryGetComponent<BarrageProjectile>(out var projectile))
            projectile.Init(origin, target, stats.damage, stats.projectileSpeed, barrageConfig.ArcHeight, projectilePool);
    }
}
