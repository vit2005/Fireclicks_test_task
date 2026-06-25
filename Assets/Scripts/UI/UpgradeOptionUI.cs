using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI descriptionLabel;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    private UpgradeConfig _config;
    private Action<UpgradeConfig> _onSelected;

    public void Setup(UpgradeConfig config, Action<UpgradeConfig> onSelected)
    {
        _config = config;
        _onSelected = onSelected;

        nameLabel.text = config.DisplayName;
        descriptionLabel.text = config.Description;
        iconImage.sprite = config.Icon;
        iconImage.gameObject.SetActive(config.Icon != null);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked() => _onSelected?.Invoke(_config);
}
