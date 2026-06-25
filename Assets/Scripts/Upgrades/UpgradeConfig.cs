using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Scriptable Objects/UpgradeConfig")]
public class UpgradeConfig : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private UpgradeEffect effect;

    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public UpgradeEffect Effect => effect;
}
