using UnityEngine;

[CreateAssetMenu(fileName = "SpellConfig", menuName = "Scriptable Objects/SpellConfig")]
public class SpellConfig : ScriptableObject
{
    [SerializeField] private SpellImplementation spellImplementation;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cooldown;

    public SpellImplementation SpellImplementation => spellImplementation;
    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public float Cooldown => cooldown;

}
