using UnityEngine;

[CreateAssetMenu(fileName = "BarrageConfig", menuName = "Scriptable Objects/BarrageConfig")]
public class BarrageConfig : SpellConfig
{
    [SerializeField] private float arcHeight = 4f;
    [Tooltip("-1 means all visible enemies")]
    [SerializeField] private int maxTargets = -1;

    public float ArcHeight => arcHeight;
    public int MaxTargets => maxTargets;
}
