using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // VARIABLES/CAMPOS
    // Referencias propias
    [SerializeField] private Image objectImg;
    [SerializeField] private TextMeshProUGUI objectText;
    [SerializeField] private GameObject toggleableObject;

    // Variables necesarias
    private Transform parentAfterDrag;
    public Transform ParentAfterDrag { get { return parentAfterDrag; } }
    private Vector3 lastPos;



    // DRAG HANDLERS--------------------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = transform.position; // Guardar la posicion actual por si se suelta fuera de un slot
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
        if (transform.parent == transform.root) // Si se suelta fuera de un slot
        {
            Debug.Log("Dropped outside a slot, returning to shadows.");
            ReturnsToTheShadows(parentAfterDrag);
        }

        // Reactivar el raycast target
        ChangeObjectRaycastState(true);
    }



    // HERRAMIENTAS--------------------------------------------------------------------------------------
    private void ChangeObjectRaycastState(bool state)
    {
        // Para que no haya conflicto al dropear o volver a ser draggable
        objectImg.raycastTarget = state;
        objectText.raycastTarget = state;
    }



    // FUNCIONES EXTERNAS PARA LLAMAR DESDE LOS SLOTS--------------------------------------------------------------------------------------
    public void SetNewParent(Transform slotTarget)
    {
        transform.SetParent(slotTarget, false);
        parentAfterDrag = slotTarget;

        toggleableObject.SetActive(!slotTarget.CompareTag("IconOnly"));
    }

    public void ReturnsToTheShadows(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.position = lastPos;
    }
}
