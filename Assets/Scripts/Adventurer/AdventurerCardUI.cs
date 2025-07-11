using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class AdventurerCardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image classTypeImage;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energySlider; 
    private AdventurerInstance Adventurer;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector2 originalPosition;
    public bool droppedInSlot = false; 


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(AdventurerInstance adventurer)
    {
        Adventurer = adventurer;

        nameText.text = Adventurer.Name;
        portraitImage.sprite = Adventurer.Portrait;
        // Actualiza el slider de salud
        healthSlider.maxValue = Adventurer.MaxHealth;
        healthSlider.value = Adventurer.CurrentHealth;

        // --- LÍNEAS A AÑADIR/MODIFICAR ---
        // Actualiza el nuevo slider de energía
        if (energySlider != null)
        {
            energySlider.maxValue = Adventurer.MaxEnergy;
            energySlider.value = Adventurer.CurrentEnergy;
        }
        rankText.text = Adventurer.Rank.ToString();
        classTypeImage.sprite = Adventurer.template.ClassTypeIcon;
    }

    public AdventurerInstance GetAdventurer()
    {
        return Adventurer;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        droppedInSlot = false; 

        transform.SetParent(originalParent.parent); 
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedInSlot)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
        canvasGroup.blocksRaycasts = true;
    }
}
