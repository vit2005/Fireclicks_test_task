using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Fireball))]
public class FireballAnimation : MonoBehaviour
{
    [SerializeField] private Fireball fireball;
    [SerializeField] private float explosionDuration = 0.5f;

    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
        fireball.OnExplode += PlayExplosion;
    }

    private void OnDestroy()
    {
        fireball.OnExplode -= PlayExplosion;
    }

    private void PlayExplosion()
    {
        transform.localScale = Vector3.one * fireball.AoeRadius;

        DOTween.Kill(transform);
        transform.DOScale(Vector3.zero, explosionDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.localScale = _originalScale;
                fireball.ReturnToPool();
            });
    }
}
