using UnityEngine;

public class FireballDirectionSectors : MonoBehaviour
{
    [SerializeField] private FireballSector[] sectors;
    [SerializeField] private float rotationSpeed = 45f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetBestDirection()
    {
        FireballSector best = null;
        int maxCount = 0;

        foreach (var sector in sectors)
        {
            int count = sector.GetCurrentEnemyCount();
            if (count > maxCount)
            {
                maxCount = count;
                best = sector;
            }
        }

        return best != null ? best.transform.forward : Vector3.zero;
    }
}
