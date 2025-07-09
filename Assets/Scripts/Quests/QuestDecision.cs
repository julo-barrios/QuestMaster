using UnityEngine;

[System.Serializable]
public class QuestDecision
{
    public string text;
    [Range(0f, 1f)] public float successChance;
    public QuestEventOutcome successOutcome;
    public QuestEventOutcome failureOutcome;
}