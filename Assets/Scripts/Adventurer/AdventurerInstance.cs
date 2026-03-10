using UnityEngine;
using System.Collections.Generic;

public class AdventurerInstance
{
    public AdventurerSO template;

    public string Name {get; set;}
    public int NextLvlXP {get; private set;}
    public int CurrentXP {get; private set;} 
    public int MaxHealth {get; set;}
    public int CurrentHealth {get; private set;}    
    public int MaxEnergy {get; private set;}
    public int CurrentEnergy {get; private set;}
    public QuestRank Rank {get; private set;} = QuestRank.E;
    public int currentStrength;
    public int XP {get; private set;}
    public bool IsResting { get; private set; } = false;
    public bool IsTired { get; private set; } = false;
    public int RecoveryDaysCounter { get; private set; } = 3;
    public bool IsDead { get; private set; } = false;
    public List<PerkSO> CurrentPerks = new List<PerkSO>();
    //public readonly List<PerkSO> InnatePerks = CurrentPerks.AsReadOnly();
    
    
    public AdventurerInstance(AdventurerSO so, int maxHealth, int currentHealth, QuestRank rank, string name)
    {
        template = so;
        currentStrength = so.baseStrength;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        MaxEnergy = 100;
        CurrentEnergy = MaxEnergy;
        Rank = rank;
        Name = name;
        
        if (so.innatePerks != null)
        {
            CurrentPerks.AddRange(so.innatePerks);
        }
    }

    public void GainXP(int amount)
    {
        CurrentXP += amount;
        if (CurrentXP >= NextLvlXP)
        {
            LevelUp();
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            IsDead = true;
        }
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void SpendEnergy(int energyCost)
    {
        CurrentEnergy -= energyCost;
        if (CurrentEnergy <= 0)
        {
            CurrentEnergy = 0;
            IsTired = true;
        }
    }
    public void SetResting(bool resting)
    {
        IsResting = resting;
    }
    public void PerformRest(int restAmount)
    {
        // Restaura la energía completamente.
        // En el futuro, podría ser más complejo (ej. recuperar 50 de energía por noche).
        CurrentEnergy = CurrentEnergy+ restAmount > MaxEnergy? CurrentEnergy+restAmount: MaxEnergy;
        IsResting = false;
        Debug.Log($"{Name} ha descansado y recuperado toda su energía.");
    }

    public void PerformNightRest()
    {
        if(IsTired)
        {
            RecoveryDaysCounter--;
            if(RecoveryDaysCounter <= 0)
            {
                IsTired = false;
                RecoveryDaysCounter = 3;
                CurrentEnergy = MaxEnergy;
            }
        }
        else
        {
            CurrentEnergy = CurrentEnergy+ 15 > MaxEnergy? CurrentEnergy+15: MaxEnergy;
        }
    }

    public void LevelUp()
    {
        switch (Rank)
        {
            case QuestRank.E:
                {
                    Rank = QuestRank.D;
                    break;
                }
            case QuestRank.D:
                {
                    Rank = QuestRank.C;
                    break;
                }
            case QuestRank.C:
                {
                    Rank = QuestRank.B;
                    break;
                }
            case QuestRank.B:
                {
                    Rank = QuestRank.A;
                    break;
                }
            case QuestRank.A:
                {
                    Rank = QuestRank.S;
                    break;
                }
            default:
                break;
        }
        NextLvlXP = NextLvlXP + 100;
        currentStrength += 5;
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        MaxEnergy += 10;
        CurrentEnergy = MaxEnergy;

    }
    
    public bool IsAvailable => !IsResting &&  !IsTired && !IsDead;
    public Sprite Portrait => template.portrait;
    public ClassType ClassType => template.classType;
}
