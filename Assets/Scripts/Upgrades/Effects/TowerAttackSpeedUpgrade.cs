using UnityEngine;

[CreateAssetMenu(fileName = "TowerAttackSpeedUpgrade", menuName = "Upgrades/Tower Attack Speed")]
public class TowerAttackSpeedUpgrade : UpgradeEffect
{
    [SerializeField] [Range(0.05f, 0.5f)] private float cooldownReduction = 0.15f;

    public override void Apply(GameContext ctx)
    {
        foreach (var slot in ctx.SpellCaster.Slots)
            slot.Stats.cooldown = Mathf.Max(0.2f, slot.Stats.cooldown * (1f - cooldownReduction));
    }
}
