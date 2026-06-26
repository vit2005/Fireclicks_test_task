# Magical Tower — Fireclicks Test Task

A Unity 3D tower defense prototype. A single tower uses spells to defeat waves of incoming enemies. The player earns upgrade choices every N kills and tries to survive as long as possible.

**Unity 6 · URP · DOTween**

---

## Architecture Overview

```
AppController
├── GameStates (InitialGameState, GameplayGameState, ResultGameState)
├── Tower
├── EnemySpawner
├── SpellCaster
├── KillTracker
└── UpgradeScreen
```

The game is driven by a simple **state machine** (`IGameState`). `AppController` owns all top-level references and passes them via `GameContext` to the upgrade system.

---

## Systems

### Game State Machine

| Class | Role |
|---|---|
| `IGameState` | Interface: `Enter()`, `Exit()` |
| `AppController` | Holds state machine, transitions between states, owns `GetRandomUpgrades()` |
| `InitialGameState` | Shows start screen |
| `GameplayGameState` | Runs gameplay: starts spawning, registers kill milestones, pauses for upgrades, ends on tower death |
| `ResultGameState` | Shows survival time on result screen |

`GameplayGameState` is the core hub — it wires together `EnemySpawner`, `KillTracker`, `UpgradeScreen`, and `Tower.OnDeath`.

---

### Health

| Class | Role |
|---|---|
| `IHealthDataProvider` | Interface supplying `MaxHealth` (implemented by `EnemyConfig`, `TowerConfig`) |
| `Health` | Tracks current HP, fires `OnDamaged` and `OnDeath` events, triggers damage popups |
| `HealthBar` | Subscribes to `Health.OnDamaged`, updates fill image |

---

### Tower

| Class | Role |
|---|---|
| `TowerConfig` | ScriptableObject: max health |
| `Tower` | Initialises `Health` from config, exposes `Health` and `OnDeath` |

---

### Enemy

| Class | Role |
|---|---|
| `EnemyConfig` | ScriptableObject: max health, speed, damage, attack rate, stop distance, scale, **weight**, **timeToSpawnAvailable** |
| `Enemy` | Composes `Health`, `EffectHandler`, `EnemyMovement`; fires `OnDeath` |
| `EnemyMovement` | Moves toward tower, attacks on interval, can be stopped |
| `EnemySpawner` | Continuously spawns enemies from weighted pools; tracks all active instances; fires `OnEnemyKilled`; returns enemies to pool on death |
| `SpawnerConfig` | ScriptableObject: spawn radius, `AnimationCurve` for spawn interval over time |

**Spawn logic:** `EnemySpawner` filters pools by `EnemyConfig.TimeToSpawnAvailable`, picks a type via weighted random, and places it outside the camera frustum using `CameraVisibility`.

---

### Effects

| Class | Role |
|---|---|
| `BaseEffect` | Abstract: `ApplyEffect(Health)`, `OnUpdate()`, `RemoveEffect()`, `IsExpired` |
| `EffectHandler` | MonoBehaviour that ticks active effects per-frame; uses a removal buffer to avoid collection mutation |
| `BurningEffect` | Concrete effect: deals damage per tick for a duration |

---

### Spells

| Class | Role |
|---|---|
| `SpellConfig` | Base ScriptableObject: name, description, icon, cooldown, damage, projectile speed |
| `SpellImplementation` | Abstract MonoBehaviour: `Cast(origin, stats, targets)` |
| `RuntimeSpellStats` | Mutable runtime copy of `SpellConfig` values (modified by upgrades) |
| `SpellCaster` | Instantiates spell prefabs as children, ticks cooldown timers, calls `Cast` when ready |

#### Fireball

| Class | Role |
|---|---|
| `FireballConfig` | Extends `SpellConfig`: AOE radius, burn damage, burn duration, burn interval |
| `FireballSector` | A trigger zone (BoxCollider); counts enemies inside via `Physics.OverlapBox` |
| `FireballDirectionSectors` | Rotates a ring of `FireballSector`s; `GetBestDirection()` picks the sector with most enemies |
| `FireballImplementation` | Casts a fireball toward the best direction; exposes `RuntimeAoeRadius` for upgrades |
| `Fireball` | Projectile: moves in a straight line, explodes on enemy or ground contact; AOE damage + `BurningEffect`; fires `OnExplode` event |
| `FireballAnimation` | `[RequireComponent(Fireball)]` — subscribes to `OnExplode`; plays DOTween scale-to-zero explosion, then returns to pool |

#### Barrage

| Class | Role |
|---|---|
| `BarrageConfig` | Extends `SpellConfig`: arc height, max targets (-1 = unlimited) |
| `BarrageImplementation` | Fires at all (or N) visible enemies simultaneously; exposes `RuntimeMaxTargets` for upgrades |
| `BarrageProjectile` | Parabolic arc projectile (Lerp + sin); tracks moving target; deals damage on arrival |

---

### Object Pooling

| Class | Role |
|---|---|
| `DefaultObjectPool` | Generic MonoBehaviour pool: `GetInstance()` / `ReleaseInstance()`; instantiates under a configurable `spawnParent` |
| `DamagePopupPool` | Singleton wrapper around `DefaultObjectPool`; static `Show(worldPos, amount, color)` |
| `ProjectilesObjectPool` | Placeholder pool component for projectile prefabs |

---

### Upgrades

| Class | Role |
|---|---|
| `UpgradeEffect` | Abstract ScriptableObject: `Apply(GameContext)` |
| `UpgradeConfig` | ScriptableObject: display name, description, icon, `UpgradeEffect` |
| `GameContext` | Readonly snapshot passed to effects: `SpellCaster`, `Tower`, `EnemySpawner` |
| `KillTracker` | Subscribes to `EnemySpawner.OnEnemyKilled`; fires `OnMilestone` every N kills |
| `UpgradeScreen` | Shows 3 random upgrade cards; fires `OnShow` event; calls callback on selection |

**Concrete effects (all ScriptableObjects):**

| Class | Effect |
|---|---|
| `TowerDamageUpgrade` | +damage to all spell slots |
| `TowerAttackSpeedUpgrade` | Reduces cooldown of all slots (min 0.2s) |
| `FireballDamageUpgrade` | +damage to the Fireball slot |
| `FireballAoeRadiusUpgrade` | Increases `FireballImplementation.RuntimeAoeRadius` |
| `BarrageDamageUpgrade` | +damage to the Barrage slot |
| `BarrageMaxTargetsUpgrade` | Increases `BarrageImplementation.RuntimeMaxTargets` |

---

### UI

| Class | Role |
|---|---|
| `DamagePopup` | Floating text on canvas; converts world position to canvas space; DOTween float-up + fade-out |
| `UpgradeOptionUI` | Single upgrade card: sets name, description, icon, button listener |
| `UpgradeScreenAnimation` | `[RequireComponent(UpgradeScreen)]` — subscribes to `OnShow`; DOScale 0→1 over 0.5s with `SetUpdate(true)` so it plays while `timeScale = 0` |

---

### Utilities

| Class | Role |
|---|---|
| `CameraVisibility` | Static helper: `IsVisible(worldPoint)` checks whether a world position falls inside the screen frustum via `WorldToScreenPoint` |

---

## Adding Content

**New enemy type:** create an `EnemyConfig` asset, set `TimeToSpawnAvailable` and `Weight`, add a pool entry to `EnemySpawner`.

**New spell:** extend `SpellConfig` and `SpellImplementation`, wire into a `SpellSlotDefinition` on `SpellCaster`.

**New upgrade:** create a class extending `UpgradeEffect`, implement `Apply(GameContext)`, create a ScriptableObject asset, add it to `AppController.upgradePool`.
