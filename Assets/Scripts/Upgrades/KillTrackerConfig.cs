using UnityEngine;

[CreateAssetMenu(fileName = "KillTrackerConfig", menuName = "Scriptable Objects/KillTrackerConfig")]
public class KillTrackerConfig : ScriptableObject
{
    [SerializeField] private int firstMilestone = 5;
    [SerializeField] private float milestoneStep = 3;

    public int FirstMilestone => firstMilestone;
    public float MilestoneStep => milestoneStep;
}
