using UnityEngine;

public class AdventurerInstance
{
    public AdventurerSO template;

    public string Name {get; set;}
     public int MaxHealth {get; set;}
    public int CurrentHealth {get; set;}    
     public int MaxEnergy {get; set;}
    public int CurrentEnergy {get; set;}
    public QuestRank Rank {get; set;}
    public int currentStrength;
    public int XP;
    public bool IsResting { get; set; } = false;
    public bool IsDead = false;

    public AdventurerInstance(AdventurerSO so, int maxHealth, int currentHealth)
    {
        template = so;
        currentStrength = so.baseStrength;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        MaxEnergy = 100;
        CurrentEnergy = MaxEnergy;
    }

    public void GainXP(int amount)
    {
        XP += amount;
    }

    public void PerformRest(int restAmount)
    {
        // Restaura la energía completamente.
        // En el futuro, podría ser más complejo (ej. recuperar 50 de energía por noche).
        CurrentEnergy = CurrentEnergy+ restAmount > MaxEnergy? CurrentEnergy+restAmount: MaxEnergy;
        IsResting = false; // Lo marcamos como no descansando, para que esté listo al día siguiente.
        Debug.Log($"{Name} ha descansado y recuperado toda su energía.");
    }
    public bool IsAvailable => !IsResting && !IsDead;
    public Sprite Portrait => template.portrait;
    public ClassType ClassType => template.classType;
}
