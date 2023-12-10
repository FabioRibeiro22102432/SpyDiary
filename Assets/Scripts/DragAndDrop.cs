using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop3D : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private bool isDragging = false;

    private void Start()
    {
        originalParent = transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Set the object as the currently dragged object
        isDragging = true;
        transform.SetParent(null); // Detach from the parent to allow free movement
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Move the object based on the pointer's movement in the world space
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                transform.position = hit.point;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Release the dragged object and reset its parent
        isDragging = false;
        transform.SetParent(originalParent);
    }
}


