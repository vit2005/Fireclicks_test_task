using System.Collections.Generic;
using UnityEngine;

public abstract class SpellImplementation : MonoBehaviour
{
    public abstract void Cast(Transform origin, RuntimeSpellStats stats, IReadOnlyList<Enemy> targets);
}
