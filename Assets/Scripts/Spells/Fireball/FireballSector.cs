using UnityEngine;

public class FireballSector : MonoBehaviour
{
    private BoxCollider _collider;

    private void Awake() => _collider = GetComponent<BoxCollider>();

    public int GetCurrentEnemyCount()
    {
        if (_collider == null) return 0;

        Vector3 worldCenter = transform.TransformPoint(_collider.center);
        Vector3 halfExtents = Vector3.Scale(_collider.size * 0.5f, transform.lossyScale);

        Collider[] hits = Physics.OverlapBox(worldCenter, halfExtents, transform.rotation);
        int count = 0;
        foreach (var hit in hits)
            if (hit.TryGetComponent<Enemy>(out _)) count++;
        return count;
    }
}
