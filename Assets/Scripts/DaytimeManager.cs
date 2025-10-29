using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DayTimeManager : MonoBehaviour
{
    public static DayTimeManager _instance;
    private static bool applicationIsQuitting = false;

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




    public static DayTimeManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            // Si la instancia aún no existe...
            if (_instance == null)
            {
                // ...buscamos el prefab en la carpeta "Resources"...
                var prefab = Resources.Load<GameObject>("GameManagers");
                // ...y lo creamos en la escena.
                Instantiate(prefab);
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        applicationIsQuitting = false;
    }

    private void Start()
    {
        StartDay(null);
    }
    private void OnEnable()
    {
        CalendarManager.OnDayAdvanced += StartDay;
    }

    // Nos desuscribimos cuando se desactiva.
    private void OnDisable()
    {
        CalendarManager.OnDayAdvanced -= StartDay;
    }
    private void Update()
    {
        if (!isDayActive || isPaused || IsGamePausedByPopup) return;

        elapsedTime += Time.deltaTime * timeMultiplier;

        if (elapsedTime >= dayDurationSeconds)
        {
            EndDay();
        }
    }

    public void StartDay(GameDate _ )
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



    private void EndDay()
    {
        isDayActive = false;
        Debug.Log("Fin del día. Transición a TavernScene.");

        // LÍNEA AÑADIDA: Carga la escena de gestión.
        SceneManager.LoadScene("TavernScene");
    }
    
    public void OnDestroy()
    {
        // Si este es el Singleton original, activamos la bandera.
        if (_instance == this)
        {
            applicationIsQuitting = true;
        }
    }

    internal float GetCurrentHour()
    {
        float dayFraction = elapsedTime / dayDurationSeconds;
        int totalHours = endHour - startHour;
        return startHour + (dayFraction * totalHours);
    }

    internal float GetDayProgress()
    {
        return elapsedTime / dayDurationSeconds;
    }
}
