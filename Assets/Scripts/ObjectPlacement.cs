using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // Drag and drop the object prefab in the Unity Editor

    void Update()
    {
        // Check for user input (e.g., mouse click)
        if (Input.GetMouseButtonDown(0)) // Change the button index as needed
        {
            // Perform a raycast from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If the ray hits something, instantiate the object at the hit point with an offset in the Y-axis
                Vector3 spawnPosition = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z); // Adjust the offset as needed
                Instantiate(objectToPlace, spawnPosition, Quaternion.identity);
            }
        }
    }
}
