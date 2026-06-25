using UnityEngine;

public class FireballDirectionSectors : MonoBehaviour
{
    private const float DebugDuration = 5f;

    [SerializeField] private FireballSector[] sectors;
    [SerializeField] private float rotationSpeed = 45f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetBestDirection(Vector3 origin, bool debug = false)
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

        if (debug)
            DrawDebug(origin, best, maxCount);

        return best != null ? best.transform.forward : Vector3.zero;
    }

    private void DrawDebug(Vector3 origin, FireballSector winner, int winnerCount)
    {
        foreach (var sector in sectors)
        {
            int count = sector.GetCurrentEnemyCount();
            Color color = sector == winner
                ? Color.green
                : count > 0 ? Color.yellow : Color.grey;

            Vector3 dir = sector.transform.forward;
            float len = 5f + count * 1.5f;
            Debug.DrawRay(origin, dir * len, color, DebugDuration);
            Debug.DrawRay(origin + dir * len, Vector3.up * 0.5f, color, DebugDuration);
        }

        if (winner != null)
            Debug.Log($"[FireballSectors] Winner: {winner.name}, enemies={winnerCount}, dir={winner.transform.forward:F2}");
        else
            Debug.Log("[FireballSectors] All sectors empty at cast time");
    }
}
