using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AsignedAdventurerContainerUI : MonoBehaviour
{
    [SerializeField] private PartySlotUI slot;
    private QuestSO currentQuestSO;
    public  void CreateNewSlots(QuestSO quest)
    {
        currentQuestSO = quest;
        var childs = gameObject.GetComponentsInChildren<PartySlotUI>();
        foreach(var childSlot in childs)
            Destroy(childSlot.gameObject);
        for(int i =0 ; i< quest.Slots; i++)
        {
            Instantiate(slot, gameObject.transform);
        }
    }

    public List<AdventurerInstance> GetAdventurers()
    {
        List<AdventurerInstance> adventurerList = new();
        var childs = gameObject.GetComponentsInChildren<PartySlotUI>();
        foreach(var childSlot in childs)
            if(childSlot.HasAdventurer)
                adventurerList.Add(childSlot.GetAdventurer());
        return adventurerList;
    }

    public bool QuestRequirementsMet()
    {
        return true;
    }

    public void ReturnToAvailable()
    {
        
        var childs = gameObject.GetComponentsInChildren<PartySlotUI>();
        foreach(var childSlot in childs)
            if(childSlot.HasAdventurer)
                childSlot.ReturnToAvailable();
    }
}
