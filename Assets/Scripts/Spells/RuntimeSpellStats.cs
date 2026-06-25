public class RuntimeSpellStats
{
    public int damage;
    public float cooldown;
    public float projectileSpeed;

    public RuntimeSpellStats(SpellConfig config)
    {
        damage = config.Damage;
        cooldown = config.Cooldown;
        projectileSpeed = config.ProjectileSpeed;
    }
}
