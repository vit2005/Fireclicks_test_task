using UnityEngine;

[CreateAssetMenu(fileName = "TowerDamageUpgrade", menuName = "Upgrades/Tower Damage")]
public class TowerDamageUpgrade : UpgradeEffect
{
    [SerializeField] private int damageIncrease = 5;

    public override void Apply(GameContext ctx)
    {
        foreach (var slot in ctx.SpellCaster.Slots)
            slot.Stats.damage += damageIncrease;
    }
}
