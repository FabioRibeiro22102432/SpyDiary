using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Canvas uiCanvas; // Reference to your UI Canvas
    public Text uiText; // Reference to any Text component you want to update on the UI

    private void Start()
    {
        // Disable the UI canvas on start
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust the tag as needed
        {
            // Show UI when the player enters the trigger zone
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(true);
            }

            // Optionally update UI text
            if (uiText != null)
            {
                uiText.text = "Press 'E' to interact";
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide UI when the player exits the trigger zone
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(false);
            }
        }
    }

    // Add any additional logic for interaction (e.g., opening a door, triggering an event) here
    // You might want to check for player input to open the UI, like pressing a key.

    // Example:
     public void Update()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
            // Handle UI opening logic here
         }
     }
}

