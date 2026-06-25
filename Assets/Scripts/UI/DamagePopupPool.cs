using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    [SerializeField] private DefaultObjectPool objectPool;

    public static DamagePopupPool Instance { get; private set; }

    private void Awake() => Instance = this;

    public static void Show(Vector3 worldPos, int amount, Color color)
    {
        if (Instance == null) return;

        GameObject obj = Instance.objectPool.GetInstance();
        if (obj.TryGetComponent<DamagePopup>(out var popup))
            popup.Show(worldPos, amount, color, Instance);
    }

    public void Release(DamagePopup popup) => objectPool.ReleaseInstance(popup.gameObject);
}
