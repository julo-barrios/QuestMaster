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

    [SerializeField] private List<AdventurerSO> _adventurerTemplates;
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

        _adventurerTemplates = new List<AdventurerSO>(Resources.LoadAll<AdventurerSO>("AdventurerTemplates"));
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
                        // 1. Crear un nuevo aventurero
            AdventurerSO template = _adventurerTemplates[UnityEngine.Random.Range(0, _adventurerTemplates.Count)];
            AdventurerInstance newRecruit = new AdventurerInstance(template, 10, 10); // Stats base
            newRecruit.Rank = (QuestRank)UnityEngine.Random.Range(1, 4); // Rango aleatorio entre E, D, C
            newRecruit.Name = "Recluta " + i; // Nombre temporal

            allAdventurers.Add(newRecruit);
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
