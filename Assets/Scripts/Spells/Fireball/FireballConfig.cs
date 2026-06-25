using UnityEngine;

[CreateAssetMenu(fileName = "FireballConfig", menuName = "Scriptable Objects/FireballConfig")]
public class FireballConfig : SpellConfig
{
    [SerializeField] private float aoeRadius = 3f;
    [SerializeField] private int burnDamage = 5;
    [SerializeField] private float burnDuration = 3f;
    [SerializeField] private float burnInterval = 1f;

    public float AoeRadius => aoeRadius;
    public int BurnDamage => burnDamage;
    public float BurnDuration => burnDuration;
    public float BurnInterval => burnInterval;
}
