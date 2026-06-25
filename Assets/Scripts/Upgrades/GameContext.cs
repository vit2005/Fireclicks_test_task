public class GameContext
{
    public readonly SpellCaster SpellCaster;
    public readonly Tower Tower;
    public readonly EnemySpawner EnemySpawner;

    public GameContext(SpellCaster spellCaster, Tower tower, EnemySpawner enemySpawner)
    {
        SpellCaster = spellCaster;
        Tower = tower;
        EnemySpawner = enemySpawner;
    }
}
