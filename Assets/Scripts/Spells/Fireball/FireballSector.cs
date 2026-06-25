using UnityEngine;

public class FireballSector : MonoBehaviour
{
    public int EnemyCount { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out _))
            EnemyCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out _))
            EnemyCount = Mathf.Max(0, EnemyCount - 1);
    }
}
