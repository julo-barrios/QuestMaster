using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Event", menuName = "Game/Quest Event")]
public class QuestEventSO : ScriptableObject
{
    [Header("Descripción del evento")]
    [TextArea]
    public string description;

    [Header("Opciones de decisión")]
    public List<QuestDecision> decisions;
}