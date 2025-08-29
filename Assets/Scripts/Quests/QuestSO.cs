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

    // --- SECCIÓN MODIFICADA ---
    [Header("Resultados de la Misión (ordenados de mejor a peor)")]
    public List<QuestResultData> successOutcomes; // Lista de posibles éxitos
    public List<QuestResultData> failureOutcomes; // Lista de posibles fracasos

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
                    duration = 0.05f;
                    break;
                }
            default:
                break;
        }
        return duration;
    }
}
