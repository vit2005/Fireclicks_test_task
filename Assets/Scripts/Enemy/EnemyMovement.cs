using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _towerTransform;
    private Tower _tower;
    private float _speed;
    private int _damage;
    private float _attackInterval;
    private float _stopDistance;
    private float _attackTimer;
    private bool _active;

    public void Init(EnemyConfig config, Tower tower)
    {
        _towerTransform = tower.transform;
        _tower = tower;
        _speed = config.Speed;
        _damage = config.Damage;
        _attackInterval = 1f / config.AttackRate;
        _stopDistance = config.StopDistance;
        _attackTimer = 0f;
        _active = true;
    }

    public void Stop() => _active = false;

    private void Update()
    {
        if (!_active) return;

        float dist = Vector3.Distance(transform.position, _towerTransform.position);

        if (dist > _stopDistance)
        {
            Vector3 dir = (_towerTransform.position - transform.position).normalized;
            transform.position += dir * (_speed * Time.deltaTime);
            transform.forward = dir;
        }
        else
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackInterval)
            {
                _attackTimer = 0f;
                _tower.Health.TakeDamage(_damage);
            }
        }
    }
}
