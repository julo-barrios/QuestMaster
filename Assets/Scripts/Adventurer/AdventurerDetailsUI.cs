using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventurerDetailsUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI inteligenceText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI luckText;

    [SerializeField] private Button fireButton;
    [SerializeField] private Button restButton;

    private AdventurerInstance _currentAdventurer;
    private TavernRosterCardUI _cardUI; // Referencia al panel que nos abrió

    public void Awake()
    {
        restButton.onClick.AddListener(OnRestButtonClicked);
        fireButton.onClick.AddListener(OnFireButtonClicked);
    }

    // Este método es llamado desde fuera para mostrar y configurar el panel.
    public void Show(AdventurerInstance adventurer, TavernRosterCardUI cardUI)
    {
        _currentAdventurer = adventurer;
        _cardUI = cardUI; // Guardamos la referencia al panel que nos abrió
        // Rellenamos los campos de la UI con los datos del aventurero.
        nameText.text = adventurer.Name;
        portraitImage.sprite = adventurer.Portrait;
        xpSlider.maxValue = adventurer.NextLvlXP;
        xpSlider.value = adventurer.CurrentXP;
        hpSlider.maxValue = adventurer.MaxHealth;
        hpSlider.value = adventurer.CurrentHealth;
        energySlider.maxValue = adventurer.MaxEnergy;
        energySlider.value = adventurer.CurrentEnergy;
        strengthText.text = adventurer.currentStrength.ToString();
        inteligenceText.text = adventurer.currentStrength.ToString();
        speedText.text = adventurer.currentStrength.ToString();
        luckText.text = adventurer.currentStrength.ToString();
        rankText.text = $"{adventurer.ClassType.ToString()} {adventurer.Rank.ToString()}";

        UpdateRestButtonText();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnRestButtonClicked()
    {
        if (_currentAdventurer == null) return;

        // Invertimos el estado de descanso
        _cardUI.RestAdventurer();
        _cardUI.UpdateStatusText(); // Actualizamos el texto de estado en la carta del aventurero
        Debug.Log($"{_currentAdventurer.Name} ahora está {(_currentAdventurer.IsResting ? "descansando" : "activo")}.");

        // Actualizamos el texto del botón para que refleje el nuevo estado
        UpdateRestButtonText();
    }
    private void UpdateRestButtonText()
    {
        if (_currentAdventurer.IsResting)
        {
            restButton.GetComponentInChildren<TextMeshProUGUI>().text = "Activar";
        }
        else
        {
            restButton.GetComponentInChildren<TextMeshProUGUI>().text = "Poner a Descansar";
        }
    }

    private void OnFireButtonClicked()
    {
        if (_currentAdventurer == null) return;

        // Invertimos el estado de descanso
        AdventurerManager.Instance.RemoveAdventurerFromRoster(_currentAdventurer);
        Debug.Log($"{_currentAdventurer.Name} ahora está {(_currentAdventurer.IsResting ? "descansando" : "activo")}.");
        Hide(); // Ocultamos el panel de detalles del aventurero
    }
}