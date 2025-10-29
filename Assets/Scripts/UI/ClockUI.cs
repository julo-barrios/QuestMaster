

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [Header("UI References")]
    public Slider dayProgressSlider;
    public TextMeshProUGUI clockText;

    private DayTimeManager _dayTimeManager;

    void Start()
    {
        _dayTimeManager = DayTimeManager.Instance;
        if (_dayTimeManager == null)
        {
            Debug.LogError("DayTimeManager instance not found!");
        }
    }

    void Update()
    {
        if (_dayTimeManager == null) return;

        if (dayProgressSlider != null)
            dayProgressSlider.value = _dayTimeManager.GetDayProgress();

        if (clockText != null)
        {
            
            float currentHour = _dayTimeManager.GetCurrentHour();

            int hours = Mathf.FloorToInt(currentHour);
            int minutes = Mathf.FloorToInt((currentHour - hours) * 60);

            clockText.text = $"{hours:D2}:{minutes:D2}";
        }
    }
}