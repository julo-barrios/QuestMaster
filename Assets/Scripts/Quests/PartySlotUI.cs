using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartySlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private GameObject EmptySlot;
    [SerializeField] private Image EmptyImage;
    private AdventurerInstance assignedAdventurer;
    private AdventurerCardUI assignedCardUI;
    private GameObject emptySlotVisual;

    public bool HasAdventurer => assignedAdventurer != null;


    public void Awake()
    {
        ClearSlot();
    }
    public void AssignAdventurer(AdventurerInstance adventurer)
    {
        assignedAdventurer = adventurer;
    }

    public void ClearSlot()
    {
        
        assignedAdventurer = null;
        emptySlotVisual = Instantiate(EmptySlot, gameObject.transform);
        emptySlotVisual.SetActive(true);

    }

    public AdventurerInstance GetAdventurer()
    {
        return assignedAdventurer;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var card = eventData.pointerDrag.GetComponent<AdventurerCardUI>();
        if (card != null )
        {
            Assign(card);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsOccupied())
        {
            ReturnToAvailable();
           
            emptySlotVisual.SetActive(true);
            //ClearSlot();
        }
    }

    public void ReturnToAvailable()
    {
        // Desasignar
        AvailableAdventurersUI.Instance.AddAvailableAdventurer(assignedAdventurer);
        // Eliminar visual de la card
        Destroy(assignedCardUI.gameObject);

        assignedAdventurer = null;
        assignedCardUI = null;
    }

    public void Assign(AdventurerCardUI cardUI)
    {
        
        if(IsOccupied())
        {
            //Remover card actual
            ReturnToAvailable();
        }

        if(emptySlotVisual.activeSelf)
        {
            emptySlotVisual.SetActive(false);
        }

        assignedAdventurer = cardUI.GetAdventurer();
        assignedCardUI = cardUI;
        
        cardUI.droppedInSlot = true; // ← AVISARLE QUE QUEDÓ EN SLOT

        // Visualmente colocar la card dentro del slot
        cardUI.transform.SetParent(this.transform, false); // <- AQUÍ

        cardUI.transform.localScale = Vector3.one * 0.6f;
        cardUI.transform.localPosition = Vector3.zero;

        cardUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
        cardUI.transform.SetAsFirstSibling();
        EmptyImage.transform.SetAsLastSibling();
    }

    public bool IsOccupied()
    {
        return assignedAdventurer != null;
    }

    public AdventurerInstance GetAssignedAdventurer()
    {
        return assignedAdventurer;
    }
}
