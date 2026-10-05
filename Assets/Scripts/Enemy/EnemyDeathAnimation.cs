using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Enemy123))]
public class EnemyDeathAnimation : MonoBehaviour
{
    [SerializeField] private Enemy123 enemy;
    [SerializeField] private float duration = 0.1f;

    private void Awake()
    {
        enemy.OnDeath += PlayDeathAnimation;
    }

    private void OnDestroy()
    {
        enemy.OnDeath -= PlayDeathAnimation;
    }

    private void PlayDeathAnimation(Enemy123 e)
    {
        DOTween.Kill(transform);
        transform.DOScale(Vector3.zero, duration)
            .SetEase(Ease.InBack)
            .OnComplete(() => enemy.NotifyReadyToReturn());
    }
}
