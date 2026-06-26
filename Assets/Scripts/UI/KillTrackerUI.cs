using TMPro;
using UnityEngine;

public class KillTrackerUI : MonoBehaviour
{
    [SerializeField] private KillTracker killTracker;
    [SerializeField] private TextMeshProUGUI label;

    private void OnEnable()
    {
        killTracker.OnMilestone += UpdateLabel;
        killTracker.OnKill += UpdateLabel;
    }

    private void OnDisable()
    {
        killTracker.OnMilestone -= UpdateLabel;
        killTracker.OnKill -= UpdateLabel;
    }

    private void UpdateLabel()
    {
        label.text = $"{killTracker.KillCount} / {killTracker.NextMilestone}";
    }
}
