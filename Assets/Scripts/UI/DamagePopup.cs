using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float Duration = 1f;
    private const float FloatHeight = 80f;

    [SerializeField] private TextMeshProUGUI label;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private DamagePopupPool _pool;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>(includeInactive: true);
    }

    public void Show(Vector3 worldPos, int amount, Color color, DamagePopupPool pool)
    {
        _pool = pool;

        label.text = amount.ToString();
        label.color = color;

        Vector2 startPos = WorldToCanvasPosition(worldPos);
        _rectTransform.anchoredPosition = startPos;

        DOTween.Kill(_rectTransform);

        _rectTransform.DOAnchorPosY(startPos.y + FloatHeight, Duration)
            .SetEase(Ease.OutQuad);

        label.DOFade(0f, Duration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => _pool.Release(this));
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
