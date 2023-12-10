using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject uiPanel;
    private bool isCursorLocked = true;


    private void Start()
    {

    

        // Lock the cursor at the start
        LockCursor();

        // Disable the UI panel at the start
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check for user input (e.g., mouse click)
        if (Input.GetKey("e"))
        {
            // Cast a ray from the screen point where the user clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the object with a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is the interactable object
                if (hit.collider.gameObject == gameObject)
                {
                    // Toggle the UI panel and cursor lock state
                    ToggleUIPanelAndCursorLock();
                }
            }
        }
    }

    void ToggleUIPanelAndCursorLock()
    {
        // Toggle the state of the UI panel
        if (uiPanel != null)
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }

        // Toggle the state of cursor lock
        isCursorLocked = !isCursorLocked;

        // Enable or disable the cursor lock based on the state
        if (isCursorLocked)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }

    }
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
