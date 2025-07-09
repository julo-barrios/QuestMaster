using UnityEngine;

[System.Serializable]
public class QuestCompletionOutcome
{
    public string label; // "Éxito", "Fracaso", etc.
    [Range(0f, 1f)] public float probability;
    public QuestEventOutcome outcome;
    public bool scaleWithDifficulty;
    public int xpGain;
    public int goldGain;
    public int fameGain;
}
