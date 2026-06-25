using UnityEngine;

[CreateAssetMenu(fileName = "SpellConfig", menuName = "Scriptable Objects/SpellConfig")]
public class SpellConfig : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cooldown;
    [SerializeField] private int damage;
    [SerializeField] private float projectileSpeed;

    public string SpellName => spellName;
    public string Description => description;
    public Sprite Icon => icon;
    public float Cooldown => cooldown;
    public int Damage => damage;
    public float ProjectileSpeed => projectileSpeed;
}
