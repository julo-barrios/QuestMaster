using System;
using System.Collections.Generic;

public class QuestOutcome
{
    public bool Success { get; }
    public string ResultLabel { get; }
    public int GoldEarned { get; }
    public int ExperienceEarned { get; }
    public int FameEarned { get; }
    public int StressChange { get; }
    public List<string> Notes { get; }

    internal QuestOutcome(
        bool success,
        string resultLabel,
        int goldEarned,
        int experienceEarned,
        int fameEarned,
        int stressChange,
        List<string> notes)
    {
        Success = success;
        ResultLabel = resultLabel;
        GoldEarned = goldEarned;
        ExperienceEarned = experienceEarned;
        FameEarned = fameEarned;
        StressChange = stressChange;
        Notes = notes;
    }
}
