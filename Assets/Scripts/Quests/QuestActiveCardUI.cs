// QuestActiveCardUI.cs (nuevo)
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestActiveCardUI : MonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public Slider progressSlider;
    public Transform partyContainer;
    public GameObject partyMemberIconPrefab;
    public void Setup(QuestInstance quest)
    {
        questTitleText.text = quest.questData.questTitle;
        UpdateProgress(0f);

        LoadPartyMembers(quest.Party);
    }

    public void UpdateProgress(float percent)
    {
        progressSlider.value = percent;
    }

        private void LoadPartyMembers(List<AdventurerInstance> party)
    {
        foreach (var member in party)
        {
            GameObject icon = Instantiate(partyMemberIconPrefab, partyContainer);
            var iconImage = icon.GetComponentInChildren<Image>();
            if (iconImage != null)
                iconImage.sprite = member.Portrait; 
        }
    }
}