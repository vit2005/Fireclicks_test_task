using UnityEngine;

[CreateAssetMenu(fileName = "BarrageDamageUpgrade", menuName = "Upgrades/Barrage Damage")]
public class BarrageDamageUpgrade : UpgradeEffect
{
    [SerializeField] private int damageIncrease = 3;

    public override void Apply(GameContext ctx)
    {
        var barrage = ctx.SpellCaster.GetComponentInChildren<BarrageImplementation>();
        if (barrage == null) return;

        var slot = ctx.SpellCaster.FindSlotForInstance(barrage);
        if (slot != null)
            slot.Stats.damage += damageIncrease;
    }
}
