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
        Vector3 targetPos = new Vector3(_tower.transform.position.x, transform.position.y, _tower.transform.position.z);
        float dist = Vector3.Distance(transform.position, targetPos);

        if (dist > _stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
            transform.forward = (targetPos - transform.position).normalized;
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
