using System;
using TMPro;
using UnityEngine;

public class GuildManager : MonoBehaviour
{
    // 1. La instancia ahora es privada.
    private static GuildManager _instance;
// --- LÍNEA AÑADIDA: El evento que se transmitirá ---
    public static event Action<int> OnGoldChanged;
    // 2. Creamos una propiedad pública para acceder a la instancia.
    public static GuildManager Instance
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

    [Header("Economía del Guild")]
    public int currentGold = 0;
    public int currentFame = 0;

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
    }

    // --- Oro ---
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"Ganaste {amount} oro. Total: {currentGold}");
        OnGoldChanged?.Invoke(currentGold);
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log($"Gastaste {amount} oro. Total restante: {currentGold}");
            OnGoldChanged?.Invoke(currentGold);
            return true;
        }
        else
        {
            Debug.Log("No tenés suficiente oro!");
            return false;
        }
    }

    // --- Fama ---
    public void AddFame(int amount)
    {
        currentFame += amount;
        Debug.Log($"Ganaste {amount} fama. Total: {currentFame}");
    }

    // --- Reglas basadas en fama ---
    public bool CanRecruitAdventurerOfRank(QuestRank rank)
    {
        // Ejemplo: cuanto más fama, mejores aventureros podés conseguir
        switch (rank)
        {
            case QuestRank.E: return true;
            case QuestRank.D: return currentFame >= 10;
            case QuestRank.C: return currentFame >= 30;
            case QuestRank.B: return currentFame >= 60;
            case QuestRank.A: return currentFame >= 100;
            case QuestRank.S: return currentFame >= 150;
            default: return false;
        }
    }
}
