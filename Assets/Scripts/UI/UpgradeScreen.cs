using System;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private UpgradeOptionUI[] optionSlots;

    public event Action OnShow;

    private Action<UpgradeConfig> _onSelected;

    public void Show(UpgradeConfig[] upgrades, Action<UpgradeConfig> onSelected)
    {
        _onSelected = onSelected;
        gameObject.SetActive(true);
        OnShow?.Invoke();

        for (int i = 0; i < optionSlots.Length; i++)
            optionSlots[i].Setup(upgrades[i], OnOptionSelected);
    }

    private void OnOptionSelected(UpgradeConfig upgrade)
    {
        gameObject.SetActive(false);
        _onSelected?.Invoke(upgrade);
    }
}
