// CalendarManager.cs
using UnityEngine;
using System; // Necesario para 'Action'

public class CalendarManager : MonoBehaviour
{
    private static CalendarManager _instance;

    public GameDate CurrentDate { get; private set; }

    // Evento que se disparará cada vez que comience un nuevo día.
    // Pasará la nueva fecha como parámetro.
    public static event Action<GameDate> OnDayAdvanced;

    public static CalendarManager Instance
    {
        get
        {
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
        DontDestroyOnLoad(gameObject);
        
        // Establecemos la fecha inicial del juego.
        InitializeDate();
    }

    private void InitializeDate()
    {
        // El juego comienza en el día 1 del mes 1 del año 1.
        CurrentDate = new GameDate(1, 1, 1);
    }

    // Este es el método que otros sistemas llamarán para pasar al siguiente día.
    public void AdvanceDay()
    {
        CurrentDate.AdvanceDay();
        Debug.Log($"AVANZA EL DÍA. Nueva fecha: {CurrentDate.ToString()}");

        // Anunciamos a todo el juego que ha pasado un día.
        OnDayAdvanced?.Invoke(CurrentDate);
    }
}