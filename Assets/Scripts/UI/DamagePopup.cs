using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float Duration = 1f;
    private const float FloatHeight = 80f; // pixels on canvas

    [SerializeField] private TextMeshProUGUI label;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private float _timer;
    private Vector2 _startAnchoredPos;
    private Color _startColor;
    private DamagePopupPool _pool;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>(includeInactive: true);
    }

    public void Show(Vector3 worldPos, int amount, Color color, DamagePopupPool pool)
    {
        _pool = pool;
        _startColor = color;
        _timer = 0f;

        label.text = amount.ToString();
        label.color = color;

        _startAnchoredPos = WorldToCanvasPosition(worldPos);
        _rectTransform.anchoredPosition = _startAnchoredPos;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / Duration);

        _rectTransform.anchoredPosition = _startAnchoredPos + Vector2.up * (FloatHeight * t);

        Color c = _startColor;
        c.a = 1f - t;
        label.color = c;

        if (_timer >= Duration)
            _pool.Release(this);
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Camera uiCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.GetComponent<RectTransform>(),
            screenPos,
            uiCamera,
            out Vector2 localPos
        );
        return localPos;
    }
}
