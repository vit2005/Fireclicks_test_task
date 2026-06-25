using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellSlotDefinition
{
    public SpellConfig config;
    public SpellImplementation prefab;
}

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private List<SpellSlotDefinition> spellDefinitions;
    [SerializeField] private EnemySpawner enemySpawner;

    private SpellSlot[] _slots;

    public IReadOnlyList<SpellSlot> Slots => _slots;

    private void Awake()
    {
        _slots = new SpellSlot[spellDefinitions.Count];

        for (int i = 0; i < spellDefinitions.Count; i++)
        {
            var def = spellDefinitions[i];
            var instance = Instantiate(def.prefab, transform);
            _slots[i] = new SpellSlot(def.config, instance);
        }
    }

    private void Update()
    {
        IReadOnlyList<Enemy> targets = enemySpawner.ActiveEnemies;

        foreach (var slot in _slots)
        {
            slot.CooldownTimer -= Time.deltaTime;

            if (slot.CooldownTimer <= 0f)
            {
                slot.CooldownTimer = slot.Stats.cooldown;
                slot.Instance.Cast(transform, slot.Stats, targets);
            }
        }
    }

    public SpellSlot GetSlot(int index) => _slots[index];

    public SpellSlot FindSlotForInstance(SpellImplementation instance)
    {
        foreach (var slot in _slots)
            if (slot.Instance == instance) return slot;
        return null;
    }
}

public class SpellSlot
{
    public readonly SpellConfig Config;
    public readonly SpellImplementation Instance;
    public readonly RuntimeSpellStats Stats;
    public float CooldownTimer;

    public SpellSlot(SpellConfig config, SpellImplementation instance)
    {
        Config = config;
        Instance = instance;
        Stats = new RuntimeSpellStats(config);
        CooldownTimer = 0f;
    }
}
