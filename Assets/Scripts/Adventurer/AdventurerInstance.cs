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
    public bool IsAvailable = true;
    public bool IsDead = false;

    public AdventurerInstance(AdventurerSO so, int maxHealth, int currentHealth)
    {
        template = so;
        currentStrength = so.baseStrength;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    public void GainXP(int amount)
    {
        XP += amount;
    }

    public Sprite Portrait => template.portrait;
    public ClassType ClassType => template.classType;
}
