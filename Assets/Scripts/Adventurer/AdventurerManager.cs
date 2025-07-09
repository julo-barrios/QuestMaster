using System;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{
    public static AdventurerManager Instance;

    [SerializeField] private List<AdventurerSO> template;
    [SerializeField] private int MaxInitialAdventurer { get; set; } = 6;
    private List<AdventurerInstance> allAdventurers = new();
    private string[] Names = new string[] { "Juan", "Pepe"};
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GenerateInitialAdventurers();
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
                Name = Names[numero2],
                IsAvailable = true
            };
            allAdventurers.Add(adv);
        }
    }

    public List<AdventurerInstance> GetAvailableAdventurers()
    {
        return allAdventurers.FindAll(a => a.IsAvailable && !a.IsDead);
    }

    public List<AdventurerInstance> GetAllAdventurers()
    {
        return allAdventurers;
    }
}
