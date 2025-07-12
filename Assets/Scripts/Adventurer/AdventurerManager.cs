using System;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{

    // 1. La instancia ahora es privada.
    private static AdventurerManager _instance;

    // 2. Creamos una propiedad pública para acceder a la instancia.
    public static AdventurerManager Instance
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

    [SerializeField] private List<AdventurerSO> template;
    [SerializeField] private int MaxInitialAdventurer { get; set; } = 6;
    private List<AdventurerInstance> allAdventurers = new();

    public static event Action OnRosterChanged;

    private string[] Names = new string[] { "Juan", "Pepe" };
    void Awake()
    {
        // El Awake ahora es mucho más simple. Se asegura de que no haya duplicados
        // y se registra a sí mismo en la variable privada.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (allAdventurers.Count == 0)
        {
            GenerateInitialAdventurers();
        }
    }

    void GenerateInitialAdventurers()
    {
        // Ejemplo de creación de aventurer
        // os

        System.Random random = new System.Random();
        int numero = random.Next(3);
        for (int i = 0; i < MaxInitialAdventurer; i++)
        {

            System.Random random2 = new System.Random();
            int numero2 = random2.Next(2); // 0 a 99
            AdventurerInstance adv = new AdventurerInstance(template[numero], 10, 10)
            {
                Name = Names[numero2]
            };
            allAdventurers.Add(adv);
        }
    }

    public List<AdventurerInstance> GetAvailableAdventurers()
    {
        return allAdventurers.FindAll(a => a.IsAvailable);
    }

    public List<AdventurerInstance> GetAllAdventurers()
    {
        return allAdventurers;
    }

    public void AddAdventurerToRoster(AdventurerInstance newAdventurer)
    {
        if (allAdventurers == null)
        {
            allAdventurers = new List<AdventurerInstance>();
        }
        allAdventurers.Add(newAdventurer);

        OnRosterChanged?.Invoke();
    }
    
    public void RemoveAdventurerFromRoster(AdventurerInstance newAdventurer)
    {
        if (allAdventurers == null)
        {
            allAdventurers = new List<AdventurerInstance>();
        }
        allAdventurers.Remove(newAdventurer);
        OnRosterChanged?.Invoke();
    }
}
