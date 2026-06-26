using System.Collections.Generic;
using UnityEngine;

public class BarrageImplementation : SpellImplementation
{
    [SerializeField] private BarrageConfig barrageConfig;
    [SerializeField] private DefaultObjectPool projectilePool;

    public int RuntimeMaxTargets { get; set; }

    private readonly List<Enemy> _visibleBuffer = new();

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

        foreach (var enemy in targets)
        {
            if (enemy != null && CameraVisibility.IsVisible(enemy.transform.position))
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
