using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float Duration = 1f;
    private const float FloatHeight = 2f;

    [SerializeField] private TextMeshPro label;

    private float _timer;
    private Vector3 _startPos;
    private Color _startColor;
    private DamagePopupPool _pool;

    public void Show(Vector3 worldPos, int amount, Color color, DamagePopupPool pool)
    {
        _startPos = worldPos;
        _startColor = color;
        _timer = 0f;
        _pool = pool;

        transform.position = worldPos;
        label.text = amount.ToString();
        label.color = color;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / Duration);

        transform.position = _startPos + Vector3.up * (FloatHeight * t);

        Color c = _startColor;
        c.a = 1f - t;
        label.color = c;

        // billboard: always face the camera
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;

        if (_timer >= Duration)
            _pool.Release(this);
    }
}
