using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Template", menuName = "Game/Quest/Quest Template")]
public class QuestSO : ScriptableObject
{
    [Header("Identificación")]
    public string questTitle;
    [TextArea]
    public string description;
    public QuestRank rank;
    public int Slots;
    public QuestUrgency Urgency;

    [Header("Requisitos")]
    public QuestRequirement requirements;

    [Header("Eventos posibles durante la misión")]
    public List<QuestEventSO> possibleEvents;

    [Header("Resultados posibles")]
    public List<QuestCompletionOutcome> completionOutcomes;

    public float DurationInDays()
    {
        float duration = 0;
        switch (Urgency)
        {
            case QuestUrgency.Prioritary:
                {
                    duration = 0.5f;
                    break;
                }
            case QuestUrgency.Secondary:
                {
                    duration = 0.3f;
                    break;
                }
            case QuestUrgency.Urgent:
                {
                    duration = 0.1f;
                    break;
                }
            default:
                break;
        }
        return duration;
    }
}
