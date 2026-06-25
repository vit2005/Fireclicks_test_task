using UnityEngine;

public class FireballDirectionSectors : MonoBehaviour
{
    [SerializeField] private FireballSector[] sectors;

    // Returns forward direction of the sector with the most enemies.
    // Returns Vector3.zero if all sectors are empty.
    public Vector3 GetBestDirection()
    {
        FireballSector best = null;
        int maxCount = 0;

        foreach (var sector in sectors)
        {
            if (sector.EnemyCount > maxCount)
            {
                maxCount = sector.EnemyCount;
                best = sector;
            }
        }

        return best != null ? best.transform.forward : Vector3.zero;
    }
}
