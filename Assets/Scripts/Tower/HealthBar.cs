using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.OnDamaged += UpdateBar;
    }

    private void OnDisable()
    {
        health.OnDamaged -= UpdateBar;
    }

    private void UpdateBar(int _)
    {
        fillImage.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
    }
}
