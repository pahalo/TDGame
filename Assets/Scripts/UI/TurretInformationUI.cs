using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TurretInformationUI : MonoBehaviour
{
    [SerializeField] private GameObject turretInfoUI; // UI panel for turret information
    [SerializeField] private Button path1Button;      // Button for Path 1
    [SerializeField] private Button path2Button;      // Button for Path 2
    [SerializeField] private Button closeButton;      // Button to close the UI
    [SerializeField] private Button sellButton;       // Button to Sell the turret
    [SerializeField] private TextMeshProUGUI turretNameText; 
    [SerializeField] private TextMeshProUGUI sellAmountText;

    private ClickPositionRaycaster clickPositionRaycaster;
    private DestroyTurret destroyTurret;
    private RaycastHit storedHit;

    private int moneyForSelling;

    private void Start()
    {
        clickPositionRaycaster = FindObjectOfType<ClickPositionRaycaster>();
        if(clickPositionRaycaster == null){
            Debug.LogError("ClickPositionRaycaster script not found in the scene.");
        }
        destroyTurret = FindObjectOfType<DestroyTurret>();
        if (destroyTurret == null)
        {
            Debug.LogError("DestroyTurret script not found in the scene.");
        }
        // Add listener for Path 1 button
        path1Button.onClick.AddListener(() => OnButtonClicked(0));

        // Add listener for Path 2 button
        path2Button.onClick.AddListener(() => OnButtonClicked(1));

        // Add listener for Close button
        closeButton.onClick.AddListener(CloseTurretInformationUI);

        // Add listener for Sell button
        sellButton.onClick.AddListener(CallSellTurret);

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
    
    // This function will call the function that will sell the turret
    private void CallSellTurret()
    {
        // Get the collider of the turret which was hit and call the sell function
        if (storedHit.collider != null)
        {
            destroyTurret.SellTurret(storedHit.collider.transform.parent.gameObject, moneyForSelling);
            CloseTurretInformationUI();
        }
        else
        {
            Debug.LogWarning("No turret selected.");
        }
    }

    // Example method to activate the UI
    public void ActivateTurretInformationUI()
    {
        turretInfoUI.SetActive(true);  // Activate the UI when this method is called
        sellAmountText.text = moneyForSelling.ToString();
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
    
    // Method to set the turret Name into the turretName textfield in the ui
    public void SetTurretName(string turretName)
    {
        if (turretNameText != null)
        {
            turretNameText.text = turretName;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI not assigned.");
        }
    }

    // Set the amount of money the player could get if he sells the turret
    public void SetTurretSellPrice(int money){
        moneyForSelling = money;
    }
}
