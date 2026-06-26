using UnityEngine;

public class BarrageProjectile : MonoBehaviour
{
    private Enemy _target;
    private Vector3 _startPos;
    private Vector3 _lastTargetPos;
    private int _damage;
    private float _arcHeight;
    private float _flightDuration;
    private float _t;
    private DefaultObjectPool _pool;

    public void Init(Vector3 startPos, Enemy target, int damage, float projectileSpeed, float arcHeight, DefaultObjectPool pool)
    {
        _startPos = startPos;
        _target = target;
        _lastTargetPos = target.transform.position;
        _damage = damage;
        _arcHeight = arcHeight;
        _pool = pool;
        _t = 0f;

        _target.RegisterIncoming(_damage);
        _target.OnDeath += OnTargetDied;

        float distance = Vector3.Distance(startPos, _lastTargetPos);
        _flightDuration = Mathf.Max(0.1f, distance / projectileSpeed);

        transform.position = startPos;
    }

    private void OnTargetDied(Enemy enemy)
    {
        enemy.OnDeath -= OnTargetDied;
        enemy.UnregisterIncoming(_damage);
        _target = null;
    }

    private void Update()
    {
        if (_target != null)
            _lastTargetPos = _target.transform.position;

        _t += Time.deltaTime / _flightDuration;

        float tClamped = Mathf.Clamp01(_t);
        Vector3 flatPos = Vector3.Lerp(_startPos, _lastTargetPos, tClamped);
        float arc = _arcHeight * Mathf.Sin(Mathf.PI * tClamped);
        transform.position = flatPos + Vector3.up * arc;

        if (_t >= 1f)
            OnReachedTarget();
    }

    private void OnReachedTarget()
    {
        if (_target != null)
        {
            _target.OnDeath -= OnTargetDied;
            _target.UnregisterIncoming(_damage);
            _target.Health.TakeDamage(_damage);
        }

        _pool.ReleaseInstance(gameObject);
    }
}
