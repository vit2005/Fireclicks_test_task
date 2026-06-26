using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    [SerializeField] private DefaultObjectPool objectPool;
    [SerializeField] private int maxPerFrame = 5;

    public static DamagePopupPool Instance { get; private set; }

    private int _spawnedThisFrame;
    private int _lastFrame;

    private void Awake() => Instance = this;

    public static void Show(Vector3 worldPos, int amount, Color color)
    {
        if (Instance == null) return;

        int frame = Time.frameCount;
        if (frame != Instance._lastFrame)
        {
            Instance._lastFrame = frame;
            Instance._spawnedThisFrame = 0;
        }

        if (Instance._spawnedThisFrame >= Instance.maxPerFrame) return;
        Instance._spawnedThisFrame++;

        GameObject obj = Instance.objectPool.GetInstance();
        if (obj.TryGetComponent<DamagePopup>(out var popup))
            popup.Show(worldPos, amount, color, Instance);
    }

    public void Release(DamagePopup popup) => objectPool.ReleaseInstance(popup.gameObject);
}
