using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TMP_Text

[System.Serializable]
public class ShipmentRequirement
{
    public string cropKey1; // Key for the first crop in the pair
    public string cropKey2; // Key for the second crop in the pair
    public int requiredAmount1; // Amount required for the first crop
    public int requiredAmount2; // Amount required for the second crop
}

public class ShipmentManager : MonoBehaviour
{
    public List<ShipmentRequirement> shipmentRequirements; // List of shipment requirements

    // UI Text components for progress
    public TMP_Text progressText1;
    public TMP_Text progressText2;
    public TMP_Text progressText3;

    // Track shipment completions
    private bool[] shipmentCompleted = new bool[3]; // Assuming 3 shipments

    void Start()
    {
        // Initialize the shipment status as false (not completed)
        for (int i = 0; i < shipmentCompleted.Length; i++)
        {
            shipmentCompleted[i] = false;
        }

        // Initialize UI text to 0/1 initially
        if (progressText1 != null) progressText1.text = "0/1";
        if (progressText2 != null) progressText2.text = "0/1";
        if (progressText3 != null) progressText3.text = "0/1";
    }

    public void TryShip(int shipmentIndex)
    {
        // Ensure we have enough crops for the shipment
        ShipmentRequirement req = shipmentRequirements[shipmentIndex];

        // Check if we have enough crops for both crop1 and crop2
        if (CounterData.GetCounter(req.cropKey1) < req.requiredAmount1 || CounterData.GetCounter(req.cropKey2) < req.requiredAmount2)
        {
            Debug.Log($"Not enough of {CounterData.GetLabel(req.cropKey1)} and {CounterData.GetLabel(req.cropKey2)} to ship!");
            return;
        }

        // Deduct the crops
        CounterData.AddToCounter(req.cropKey1, -req.requiredAmount1);
        CounterData.AddToCounter(req.cropKey2, -req.requiredAmount2);

        // Mark shipment as completed
        shipmentCompleted[shipmentIndex] = true;

        // Update the UI to reflect the shipment status
        UpdateProgressTexts(shipmentIndex);

        // Check if all shipments are completed
        if (AllShipmentsCompleted())
        {
            EndGame();
        }
    }

    private void UpdateProgressTexts(int shipmentIndex)
    {
        // Update the progress text for the corresponding shipment
        if (shipmentIndex == 0 && progressText1 != null)
        {
            progressText1.text = "1/1"; // Update progress to 1/1 for completed shipment
        }
        else if (shipmentIndex == 1 && progressText2 != null)
        {
            progressText2.text = "1/1";
        }
        else if (shipmentIndex == 2 && progressText3 != null)
        {
            progressText3.text = "1/1";
        }
    }

    private bool AllShipmentsCompleted()
    {
        // Check if all shipments have been completed
        foreach (bool completed in shipmentCompleted)
        {
            if (!completed) return false;
        }
        return true;
    }

    private void EndGame()
    {
        Debug.Log("All shipments completed! You win!");
        // Implement game end logic, like showing a win screen or ending the game
        // Example: 
        // SceneManager.LoadScene("GameOverScene");
        // Or show a message:
        if (progressText1 != null) progressText1.text = "You Win!";
        if (progressText2 != null) progressText2.text = "You Win!";
        if (progressText3 != null) progressText3.text = "You Win!";
    }
}
