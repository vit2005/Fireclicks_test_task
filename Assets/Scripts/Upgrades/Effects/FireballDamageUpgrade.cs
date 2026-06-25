using UnityEngine;

[CreateAssetMenu(fileName = "FireballDamageUpgrade", menuName = "Upgrades/Fireball Damage")]
public class FireballDamageUpgrade : UpgradeEffect
{
    [SerializeField] private int damageIncrease = 10;

    public override void Apply(GameContext ctx)
    {
        var fireball = ctx.SpellCaster.GetComponentInChildren<FireballImplementation>();
        if (fireball == null) return;

        var slot = ctx.SpellCaster.FindSlotForInstance(fireball);
        if (slot != null)
            slot.Stats.damage += damageIncrease;
    }
}
