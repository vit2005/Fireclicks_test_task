using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDeathAnimation : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float duration = 0.1f;

    private void Awake()
    {
        enemy.OnDeath += PlayDeathAnimation;
    }

    private void OnDestroy()
    {
        enemy.OnDeath -= PlayDeathAnimation;
    }

    private void PlayDeathAnimation(Enemy e)
    {
        DOTween.Kill(transform);
        transform.DOScale(Vector3.zero, duration)
            .SetEase(Ease.InBack)
            .OnComplete(() => enemy.NotifyReadyToReturn());
    }
}
