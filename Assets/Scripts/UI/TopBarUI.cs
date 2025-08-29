using UnityEngine;
using TMPro;
using UnityEngine.UI; // No olvidar para el texto

public class TopBarUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI guildNameText;
    [SerializeField] private TextMeshProUGUI guildLevelText;
    [SerializeField] private Slider leveSlider;
    // (Puedes añadir aquí las referencias a tu slider de Fama, texto de Nivel, etc. en el futuro)

    // Este método se ejecuta cuando el objeto se activa.
    private void OnEnable()
    {
        if (GuildManager.Instance != null)
        {
            SubscribeToEvents();
            UpdateInitialUI();
        }
        else
        {
            // Si el GuildManager no está listo, esperamos un frame y lo intentamos de nuevo.
            Invoke(nameof(OnEnable), 0.01f);
        }
    }

    // Este método se ejecuta cuando el objeto se desactiva.
    private void OnDisable()
    {
        // Es MUY IMPORTANTE desuscribirse para evitar errores y fugas de memoria.
        if (GuildManager.Instance != null)
        {
            GuildManager.OnGoldChanged -= UpdateGoldText;
            GuildManager.Instance.Progression.OnXPChanged -= UpdateXPSlider;
            GuildManager.Instance.Progression.OnLevelUp -= UpdateLevelText;
        }
    }

    private void SubscribeToEvents()
    {
        
        GuildManager.OnGoldChanged += UpdateGoldText;
        GuildManager.Instance.Progression.OnXPChanged += UpdateXPSlider;
        GuildManager.Instance.Progression.OnLevelUp += UpdateLevelText;
    }
    private void UpdateInitialUI()
    {
        goldText.text = GuildManager.Instance.currentGold.ToString();
        guildNameText.text = GuildManager.Instance.GuildName;
        leveSlider.value = GuildManager.Instance.Progression.CurrentXP;
                // Actualizamos la UI con los valores iniciales al empezar.
        var progression = GuildManager.Instance.Progression;
        var nextLevelData = ProgressionManager.Instance.GetLevelData(ProgressionType.Guild, progression.CurrentLevel + 1);
        int xpForNext = nextLevelData != null ? nextLevelData.XP_Required : progression.CurrentXP;

        UpdateXPSlider(progression.CurrentXP, xpForNext);
        UpdateLevelText(progression.CurrentLevel);
    }
    void Start()
    {
        goldText.text = GuildManager.Instance.currentGold.ToString();
        guildNameText.text = GuildManager.Instance.GuildName;
        leveSlider.value = GuildManager.Instance.Progression.CurrentXP;
    }

    // El método que reacciona al evento.
    private void UpdateGoldText(int newAmount)
    {
        goldText.text = newAmount.ToString();
    }
    
    private void UpdateXPSlider(int currentXP, int xpForNextLevel)
    {
        // Obtenemos la XP necesaria para el nivel actual (el inicio de la barra).
        LevelData currentLevelData = ProgressionManager.Instance.GetLevelData(ProgressionType.Guild, GuildManager.Instance.Progression.CurrentLevel);
        int xpForCurrentLevel = currentLevelData.XP_Required;

        // Calculamos el rango de XP para el nivel actual.
        float totalXpInThisLevel = xpForNextLevel - xpForCurrentLevel;

        // Calculamos cuánto hemos progresado DENTRO de este nivel.
        float xpProgress = currentXP - xpForCurrentLevel;

        // Si estamos en el nivel máximo, la barra se llena.
        if (totalXpInThisLevel <= 0)
        {
            leveSlider.value = 1;
        }
        else
        {
            // Asignamos el valor al slider (se normaliza automáticamente entre 0 y 1).
            leveSlider.value = xpProgress / totalXpInThisLevel;
        }
    }

    // Este método se ejecuta cuando subimos de nivel.
    private void UpdateLevelText(int newLevel)
    {
        if (guildLevelText != null)
        {
            guildLevelText.text = $"{newLevel}";
        }
    }
}