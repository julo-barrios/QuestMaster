using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitCardUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image classIconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI costText;
    public Button hireButton; // Público para que el panel lo configure

    public void Setup(AdventurerInstance adventurer, int cost)
    {
        portraitImage.sprite = adventurer.Portrait;
        nameText.text = adventurer.Name;
        rankText.text = adventurer.Rank.ToString();
        costText.text = cost.ToString() + " <sprite name=gold_icon>"; // Asumiendo que tienes un ícono de oro en TextMeshPro
    }
}