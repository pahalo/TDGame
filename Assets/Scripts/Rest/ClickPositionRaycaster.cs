using UnityEngine;

public class ClickPositionRaycaster : MonoBehaviour
{
    private BuildTurrets buildTurrets;

    void Start()
    {
        // Find the BuildTurrets script attached to the same GameObject
        buildTurrets = FindObjectOfType<BuildTurrets>();

        if (buildTurrets == null)
        {
            Debug.LogError("BuildTurrets script not found in the scene.");
        }
    }

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
                if (IsClickOnTurret(hit))
                {
                    // Get the parent object of the clicked collider because the collider is on the no build zone range
                    Transform parentTransform = hit.collider.transform.parent;

                    if (parentTransform != null)
                    {
                        // Get the TurretStats component from the parent object
                        TurretStats turretStats = parentTransform.GetComponent<TurretStats>();
                        if (turretStats != null)
                        {   
                            int turretID = turretStats.turretID;
                            Debug.Log("Clicked on turret with ID: " + turretID);
                        }
                        else
                        {
                            Debug.LogWarning("No TurretStats component found on the parent object of the clicked turret.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("The clicked object has no parent.");
                    }
                }
                else
                {
                    Debug.Log("Hit Position: " + hit.point);
                    buildTurrets.PlaceTurretAtPosition(hit.point);
                }
            }
        }
    }

    // return true if the colliders Tag is turret to know if it is a turret
    private bool IsClickOnTurret(RaycastHit hit)
    {
        // Check if the clicked object is tagged as "Turret"
        return hit.collider.CompareTag("Turret");
    }
}
