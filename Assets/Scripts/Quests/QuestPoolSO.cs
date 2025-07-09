using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestPoolSO", menuName = "Game/QuestPoolSO")]
public class QuestPoolSO : ScriptableObject
{
    public List<QuestSO> availableQuests;

    public QuestSO GetRandomQuest()
    {
        return availableQuests[Random.Range(0, availableQuests.Count)];
    }
}
