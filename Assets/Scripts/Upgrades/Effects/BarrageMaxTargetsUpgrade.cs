using UnityEngine;

[CreateAssetMenu(fileName = "BarrageMaxTargetsUpgrade", menuName = "Upgrades/Barrage Max Targets")]
public class BarrageMaxTargetsUpgrade : UpgradeEffect
{
    [SerializeField] private int additionalTargets = 2;

    public override void Apply(GameContext ctx)
    {
        var barrage = ctx.SpellCaster.GetComponentInChildren<BarrageImplementation>();
        if (barrage != null)
            barrage.RuntimeMaxTargets += additionalTargets;
    }
}
