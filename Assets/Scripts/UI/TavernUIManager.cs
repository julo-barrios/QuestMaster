using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernUIManager : MonoBehaviour
{
    [Header("Paneles de Contenido")]
    [SerializeField] private GameObject rosterPanel;
    [SerializeField] private GameObject recruitmentPanel;
    [SerializeField] private GameObject upgradesPanel;

    void Start()
    {
        // Al empezar, nos aseguramos de que solo el panel de la plantilla esté visible.
        ShowRosterPanel();
        var gold = GuildManager.Instance.currentGold;
    }

    // --- Métodos para los botones de las pestañas ---

    public void ShowRosterPanel()
    {
        rosterPanel.SetActive(true);
        recruitmentPanel.SetActive(false);
        upgradesPanel.SetActive(false);
    }

    public void ShowRecruitmentPanel()
    {
        rosterPanel.SetActive(false);
        recruitmentPanel.SetActive(true);
        upgradesPanel.SetActive(false);
    }

    public void ShowUpgradesPanel()
    {
        rosterPanel.SetActive(false);
        recruitmentPanel.SetActive(false);
        upgradesPanel.SetActive(true);
    }

    // --- Método para el botón de fin de día ---
    private void ProcessNightlyActions()
    {
        Debug.Log("Procesando acciones nocturnas...");
        List<AdventurerInstance> allAdventurers = AdventurerManager.Instance.GetAllAdventurers();

        foreach (var adventurer in allAdventurers)
        {
            if (adventurer.IsResting)
            {
                adventurer.PerformRest(15);
            }
        }
    }
    public void StartNewDay()
    {
        ProcessNightlyActions();
        Debug.Log("Comenzando nuevo día. Cargando MapScene...");
        SceneManager.LoadScene("MapScene");
    }
}