using UnityEngine;

[CreateAssetMenu(fileName = "FireballAoeRadiusUpgrade", menuName = "Upgrades/Fireball AOE Radius")]
public class FireballAoeRadiusUpgrade : UpgradeEffect
{
    [SerializeField] private float radiusIncrease = 1f;

    public override void Apply(GameContext ctx)
    {
        var fireball = ctx.SpellCaster.GetComponentInChildren<FireballImplementation>();
        if (fireball != null)
            fireball.RuntimeAoeRadius += radiusIncrease;
    }
}
