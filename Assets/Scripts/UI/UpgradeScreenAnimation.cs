using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UpgradeScreen))]
public class UpgradeScreenAnimation : MonoBehaviour
{
    [SerializeField] private UpgradeScreen upgradeScreen;

    private void Awake()
    {
        upgradeScreen.OnShow += PlayShowAnimation;
    }

    private void OnDestroy()
    {
        upgradeScreen.OnShow -= PlayShowAnimation;
    }

    private void PlayShowAnimation()
    {
        transform.localScale = Vector3.zero;
        DOTween.Kill(transform);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
    }
}
