using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPositionRaycaster : MonoBehaviour
{
    private BuildTurrets buildTurrets;
    private UpgradeTurrets upgradeTurrets;
    private TurretInformationUI turretInformationUI;

    private int selectedPathIndex = -1; // Safe the upgrade path variable

    void Start()
    {
        // Find the BuildTurrets script attached to the same GameObject
        buildTurrets = FindObjectOfType<BuildTurrets>();
        upgradeTurrets = FindObjectOfType<UpgradeTurrets>();
        if (buildTurrets == null)
        {
            Debug.LogError("BuildTurrets script not found in the scene.");
        }
        if(upgradeTurrets == null)
        {
            Debug.LogError("UpgradeTurrets script not found in the scene.");
        }
    }

    void Update()
    {   
        // If the game is paused there should be no inputs
        if (PauseMenu.gameIsPaused == true) { return; }

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
            // Check if the player clicked on a UI Element
            if (EventSystem.current.IsPointerOverGameObject()){  return; }

            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast and check if it hits something
            if (Physics.Raycast(ray, out hit))
            {
                // If there is no turret, build it
                if (!IsClickOnTurret(hit))
                {
                    ProcessTurretInteraction(hit);
                } else
                {
                    // Find the turretInformationUI script here, because that way it will be the one in the scene
                    turretInformationUI = FindObjectOfType<TurretInformationUI>();

                    // Activate the turretInformationUI
                    if (turretInformationUI != null)
                    {
                        Transform parentTransform = hit.collider.transform.parent;
                        TurretStats turretStats = parentTransform?.GetComponent<TurretStats>();
                        if (turretStats != null)
                        {
                            // Setting the Name into the turret UI but removing the (clone) at the end which is there because it is a prefab
                            string turretName = parentTransform.name.Replace("(Clone)", "");
                            turretInformationUI.SetTurretName(turretName);

                            // Getting the turret build cost and setting the amount of money the player will get for selling it
                            int sellAmount = turretStats.GetTurretCost() /  GameManager.Instance.FactorOfSellingOnMoney;;
                            turretInformationUI.SetTurretSellPrice(sellAmount);
                        }
                        turretInformationUI.ActivateTurretInformationUI();
                        turretInformationUI.SetRaycastHit(hit);
                    }
                    else
                    {
                        Debug.LogWarning("TurretInformationUI script not found in the scene.");
                    }
                }
        }
    }
    }
    // Check for turret selection keys and upgrade path selection keys
    public void SelectTurret(int index){
        buildTurrets.SelectTurretType(index);
        selectedPathIndex = index;
    }

    // This method is called through the buttons in the turret information ui and calls the script that will upgrade the turret
    public void ProcessTurretInteraction(RaycastHit hit)
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

    // Return true if the collider's tag is "Turret" to know if it is a turret
    private bool IsClickOnTurret(RaycastHit hit)
    {
        // Check if the clicked object is tagged as "Turret"
        return hit.collider.CompareTag("Turret");
    }
}
