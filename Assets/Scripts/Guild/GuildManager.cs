using TMPro;
using UnityEngine;

public class GuildManager : MonoBehaviour
{
    public static GuildManager Instance;

    [Header("Economía del Guild")]
    public int currentGold = 0;
    public int currentFame = 0;

    [SerializeField] private TextMeshProUGUI textoOro;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // --- Oro ---
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"Ganaste {amount} oro. Total: {currentGold}");
        textoOro.text = currentGold.ToString();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log($"Gastaste {amount} oro. Total restante: {currentGold}");
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

    public void LoseFame(int amount)
    {
        currentFame -= amount;
        if (currentFame < 0) currentFame = 0;
        Debug.Log($"Perdiste {amount} fama. Total: {currentFame}");
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
