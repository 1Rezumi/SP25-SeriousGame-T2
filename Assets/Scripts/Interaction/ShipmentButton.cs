using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipmentButton : MonoBehaviour
{
    // First crop pair slot
    public string cropKey1;      // First crop key for shipment
    public int requiredAmount1;  // Required amount for the first crop

    // Second crop pair slot
    public string cropKey2;      // Second crop key for shipment
    public int requiredAmount2;  // Required amount for the second crop

    // Text that displays progress for both crops
    public TMP_Text progressText1;  // Progress text for the first crop
    public TMP_Text progressText2;  // Progress text for the second crop

    // Button to trigger the shipment
    public Button button;

    private void Start()
    {
        // Set the button click listener
        button.onClick.AddListener(OnButtonPressed);

        // Initial progress update
        UpdateProgressText();
    }

    private void OnButtonPressed()
    {
        // Check if there are enough crops for both crop1 and crop2
        if (CounterData.GetCounter(cropKey1) >= requiredAmount1 && CounterData.GetCounter(cropKey2) >= requiredAmount2)
        {
            // Deduct the crops from the inventory
            CounterData.AddToCounter(cropKey1, -requiredAmount1);
            CounterData.AddToCounter(cropKey2, -requiredAmount2);

            // Update the UI to reflect the progress
            UpdateProgressText();

            // Log completion message
            Debug.Log($"Shipment completed for {CounterData.GetLabel(cropKey1)} + {CounterData.GetLabel(cropKey2)}!");
        }
        else
        {
            // Log not enough crops
            Debug.Log("Not enough crops for shipment.");
        }
    }

    // Update the UI to show the current crop count vs required amount for both crops
    private void UpdateProgressText()
    {
        // Update progress for the first crop (Crop 1)
        if (progressText1 != null)
            progressText1.text = $"{CounterData.GetLabel(cropKey1)}: {CounterData.GetCounter(cropKey1)} / {requiredAmount1}";

        // Update progress for the second crop (Crop 2)
        if (progressText2 != null)
            progressText2.text = $"{CounterData.GetLabel(cropKey2)}: {CounterData.GetCounter(cropKey2)} / {requiredAmount2}";
    }
}
