using UnityEngine;
using UnityEngine.UI;

public class TurretInformationUI : MonoBehaviour
{
    [SerializeField] private GameObject turretInfoUI; // UI panel for turret information
    [SerializeField] private Button path1Button;      // Button for Path 1
    [SerializeField] private Button path2Button;      // Button for Path 2
    [SerializeField] private Button closeButton;      // Button to close the UI

    private ClickPositionRaycaster clickPositionRaycaster;
    private RaycastHit storedHit;

    private void Start()
    {
        clickPositionRaycaster = FindObjectOfType<ClickPositionRaycaster>();
        if(clickPositionRaycaster == null){
            Debug.LogError("ClickPositionRaycaster script not found in the scene.");
        }
        // Add listener for Path 1 button
        path1Button.onClick.AddListener(() => OnButtonClicked(0));

        // Add listener for Path 2 button
        path2Button.onClick.AddListener(() => OnButtonClicked(1));

        // Add listener for Close button
        closeButton.onClick.AddListener(CloseTurretInformationUI);

        // Ensure the UI is inactive at the start
        turretInfoUI.SetActive(false);
    }

    // This function is called when a button is clicked
    private void OnButtonClicked(int index)
    {
        clickPositionRaycaster.SelectTurret(index);
        clickPositionRaycaster.ProcessTurretInteraction(storedHit);
        CloseTurretInformationUI();
    }

    // Example method to activate the UI
    public void ActivateTurretInformationUI()
    {
        turretInfoUI.SetActive(true);  // Activate the UI when this method is called
    }

    // This function is called when the Close button is clicked
    private void CloseTurretInformationUI()
    {
        turretInfoUI.SetActive(false);  // Deactivate the UI when this method is called
    }

    // Setter-Methode to safe the raycast value where the player tapped on the screen
    public void SetRaycastHit(RaycastHit hit)
    {
        storedHit = hit; 
    }
}
