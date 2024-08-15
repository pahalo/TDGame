using UnityEngine;

public class ClickPositionRaycaster : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast and check if it hits something
            if (Physics.Raycast(ray, out hit))
            {
                // Log the coordinates of the hit point
                Debug.Log("Hit Position: " + hit.point);
            }
        }
    }
}
