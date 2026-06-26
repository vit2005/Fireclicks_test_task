using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Prefab requires: SphereCollider (isTrigger=true), Rigidbody (isKinematic=true, useGravity=false)
    // Ground GameObject must have tag "Ground"

    private const float MaxLifetime = 8f;

    public event Action OnExplode;

    public float AoeRadius => _aoeRadius;

    private Vector3 _direction;
    private float _speed;
    private int _damage;
    private FireballConfig _config;
    private float _aoeRadius;
    private DefaultObjectPool _pool;
    private bool _exploded;
    private float _lifetime;

    public void Init(Vector3 startPos, Vector3 direction, int damage, float speed,
        FireballConfig config, DefaultObjectPool pool, float aoeRadius)
    {
        transform.position = startPos;
        _direction = direction.normalized;
        _speed = speed;
        _damage = damage;
        _config = config;
        _aoeRadius = aoeRadius;
        _pool = pool;
        _exploded = false;
        _lifetime = 0f;
    }

    private void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);

        _lifetime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_exploded) return;

        if (other.TryGetComponent<Enemy>(out _) || other.CompareTag("Ground"))
        {
            _exploded = true;
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _aoeRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Health.TakeDamage(_damage);
                enemy.EffectHandler.AddEffect(
                    new BurningEffect(_config.BurnDamage, _config.BurnDuration, _config.BurnInterval));
            }
        }

        if (OnExplode != null)
            OnExplode.Invoke();
        else
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        _pool.ReleaseInstance(gameObject);
    }
}
