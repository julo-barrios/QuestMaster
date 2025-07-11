using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DayTimeManager : MonoBehaviour
{
    public static DayTimeManager Instance;

    //TODO REFACTOR
    public bool IsGamePausedByPopup = false;

    [Header("Time Settings")]
    public int startHour = 8; // 8 AM
    public int endHour = 20;  // 8 PM
    public float dayDurationSeconds = 300f; // 5 minutos reales = 12 horas in-game
    private float elapsedTime = 0f;
    private bool isDayActive = false;
    private bool isPaused = false;
    private float timeMultiplier = 1f;

    [Header("UI References")]
    public Slider dayProgressSlider;
    public TextMeshProUGUI clockText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartDay();
    }

    private void Update()
    {
        if (!isDayActive || isPaused || IsGamePausedByPopup) return;

        elapsedTime += Time.deltaTime * timeMultiplier;
        UpdateUI();

        if (elapsedTime >= dayDurationSeconds)
        {
            EndDay();
        }
    }

    public void StartDay()
    {
        elapsedTime = 0f;
        isDayActive = true;
        isPaused = false;
    }

    public void PauseDay()
    {
        isPaused = true;
    }

    public void ResumeDay()
    {
        isPaused = false;
    }
    public bool IsPaused()
    {
        return isPaused;
    }
    
    public void AccelerateTime(float multiplier)
    {
        timeMultiplier = multiplier;
    }

    private void UpdateUI()
    {
        if (dayProgressSlider != null)
            dayProgressSlider.value = elapsedTime / dayDurationSeconds;

        if (clockText != null)
        {
            float dayFraction = elapsedTime / dayDurationSeconds;
            int totalHours = endHour - startHour;
            float currentHour = startHour + dayFraction * totalHours;

            int hours = Mathf.FloorToInt(currentHour);
            int minutes = Mathf.FloorToInt((currentHour - hours) * 60);

            clockText.text = $"{hours:D2}:{minutes:D2}";
        }
    }

    private void EndDay()
    {
        isDayActive = false;
        Debug.Log("Fin del día. Transición a TavernScene.");

        // LÍNEA AÑADIDA: Carga la escena de gestión.
        SceneManager.LoadScene("TavernScene");
    }
}
