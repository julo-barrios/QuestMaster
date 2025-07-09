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
    private AdventurerInstance Adventurer;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector2 originalPosition;
    public bool droppedInSlot = false; // ← NUEVO


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
        healthSlider.maxValue = Adventurer.MaxHealth;
        healthSlider.value = Adventurer.CurrentHealth;
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
        droppedInSlot = false; // ← Resetear

        transform.SetParent(originalParent.parent); // Mover por encima de todo
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
