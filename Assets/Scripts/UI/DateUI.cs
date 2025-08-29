using UnityEngine;
using TMPro; 

public class DateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;

    // Se suscribe a los eventos cuando el objeto se activa.
    private void OnEnable()
    {
        CalendarManager.OnDayAdvanced += UpdateDateText;
        
        // También actualizamos el texto con la fecha actual al activarse.
        if (CalendarManager.Instance != null)
        {
            UpdateDateText(CalendarManager.Instance.CurrentDate);
        }
    }

    // Se desuscribe para evitar errores.
    private void OnDisable()
    {
        CalendarManager.OnDayAdvanced -= UpdateDateText;
    }

    // Este método se ejecuta automáticamente cuando el CalendarManager anuncia un nuevo día.
    private void UpdateDateText(GameDate newDate)
    {
        dateText.text = newDate.ToString();
    }
}