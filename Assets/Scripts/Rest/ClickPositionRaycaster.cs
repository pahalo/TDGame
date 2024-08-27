using UnityEngine;

public class ClickPositionRaycaster : MonoBehaviour
{
    private BuildTurrets buildTurrets;
    private UpgradeTurrets upgradeTurrets;

    private int selectedPathIndex = -1; // Variable zum Speichern des ausgewählten Upgrade-Pfads

    void Start()
    {
        // Find the BuildTurrets script attached to the same GameObject
        buildTurrets = FindObjectOfType<BuildTurrets>();
        upgradeTurrets = FindObjectOfType<UpgradeTurrets>();

        if (buildTurrets == null)
        {
            Debug.LogError("BuildTurrets script not found in the scene.");
        }
    }

    void Update()
    {
        // If the game is paused there should be no inputs
        if(PauseMenu.gameIsPaused == true) { return; }
        // Check for turret selection keys and upgrade path selection keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildTurrets.SelectTurretType(0);
            selectedPathIndex = 0;
            Debug.Log("Selected Turret Type 1 and Upgrade Path 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildTurrets.SelectTurretType(1);
            selectedPathIndex = 1;
            Debug.Log("Selected Turret Type 2 and Upgrade Path 2");
        }

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
                            if (selectedPathIndex >= 0 && selectedPathIndex < turretStats.upgradePaths.Count)
                            {
                                // Upgrade the turret with the selected path
                                upgradeTurrets.UpgradeTurret(turretStats, selectedPathIndex);
                            }
                            else
                            {
                                Debug.LogWarning("Invalid Upgrade Path selected.");
                            }
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
                    buildTurrets.PlaceTurretAtPosition(hit.point, false);
                }
            }
        }
    }

    // Return true if the collider's tag is "Turret" to know if it is a turret
    private bool IsClickOnTurret(RaycastHit hit)
    {
        // Check if the clicked object is tagged as "Turret"
        return hit.collider.CompareTag("Turret");
    }
}
