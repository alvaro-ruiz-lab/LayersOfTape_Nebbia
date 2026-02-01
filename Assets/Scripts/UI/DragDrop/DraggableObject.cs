using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // VARIABLES/CAMPOS
    // Referencias propias
    [SerializeField] private TextMeshProUGUI objectText;

    // Variables necesarias
    private Transform parentAfterDrag;
    public Transform ParentAfterDrag { get { return parentAfterDrag; } }



    // DRAG HANDLERS--------------------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent; /* Guardar el padre inicial por si se suelta fuera de un slot, volver aqui */
        transform.SetParent(transform.root); /* Llevar objeto al root */
        transform.SetAsLastSibling(); /* Para que se vea por encima de cualquier elemento del Canvas */
        ChangeObjectRaycastState(false); // Desactivar el raycast target para que no se detecte a si mismo al soltar
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // El objeto sigue al puntero 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Si se suelta en un slot, este maneja el hacerlo su hijo
        if (transform.parent == transform.root || transform.parent.CompareTag("Slot")) // Si se suelta fuera de un slot
        {
            ReturnsToTheShadows(parentAfterDrag);
        }

        // Reactivar el raycast target
        ChangeObjectRaycastState(true);
    }



    // HERRAMIENTAS--------------------------------------------------------------------------------------
    private void ChangeObjectRaycastState(bool state)
    {
        // Para que no haya conflicto al dropear o volver a ser draggable
        objectText.raycastTarget = state;
    }



    // FUNCIONES EXTERNAS PARA LLAMAR DESDE LOS SLOTS--------------------------------------------------------------------------------------
    public void SetNewParent(Transform slotTarget)
    {
        transform.SetParent(slotTarget, false);
        parentAfterDrag = slotTarget;
    }

    public void ReturnsToTheShadows(Transform newParent)
    {
        transform.SetParent(newParent);
    }
}
