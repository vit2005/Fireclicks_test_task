using UnityEngine;

[CreateAssetMenu(fileName = "KillTrackerConfig", menuName = "Scriptable Objects/KillTrackerConfig")]
public class KillTrackerConfig : ScriptableObject
{
    [SerializeField] private int firstMilestone = 5;
    [SerializeField] private int milestoneStep = 3;

    public int FirstMilestone => firstMilestone;
    public int MilestoneStep => milestoneStep;
}
