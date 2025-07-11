using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TavernRosterCardUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image classIconImage;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] public Button manageButton; // Lo hacemos público para acceder desde fuera

    private AdventurerInstance _adventurer;

    public void Setup(AdventurerInstance adventurer)
    {
        _adventurer = adventurer;

        // Rellenamos todos los campos con la información del aventurero
        portraitImage.sprite = adventurer.Portrait;
        nameText.text = adventurer.Name;
        rankText.text = adventurer.Rank.ToString(); // Podríamos mejorarlo para mostrar estrellas
        classIconImage.sprite = adventurer.template.ClassTypeIcon;

        healthSlider.maxValue = adventurer.MaxHealth;
        healthSlider.value = adventurer.CurrentHealth;

        energySlider.maxValue = adventurer.MaxEnergy;
        energySlider.value = adventurer.CurrentEnergy;

        // Actualizamos el texto de estado
        UpdateStatusText();
    }
    public void RestAdventurer()
    {
        _adventurer.IsResting = !_adventurer.IsResting;
    }
    public void UpdateStatusText()
    {
        // Esta lógica determinará qué texto mostrar
        if (_adventurer.IsResting) // Suponiendo que añadiremos 'IsResting' en la Tarea 7
        {
            statusText.text = "Descansando";
            statusText.color = Color.cyan;
        }
        else if (_adventurer.CurrentEnergy <= 30)
        {
            statusText.text = "Agotado";
            statusText.color = Color.yellow;
        }
        else
        {
            statusText.text = "Disponible";
            statusText.color = Color.green;
        }
    }
}