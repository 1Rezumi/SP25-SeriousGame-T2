using UnityEngine;

public class DevShipmentFiller : MonoBehaviour
{
    [Header("Crop Requirements for Shipment")]
    public string cropKey1; // For example: "Crop1"
    public int requiredAmount1;

    public string cropKey2; // For example: "Crop2"
    public int requiredAmount2;

    public string cropKey3; // For example: "Crop3"
    public int requiredAmount3;

    public void FillCropsForShipment()
    {
        FillCrop(cropKey1, requiredAmount1);
        FillCrop(cropKey2, requiredAmount2);
        FillCrop(cropKey3, requiredAmount3);

        // Optionally update UI counters
        foreach (var display in FindObjectsOfType<CounterDisplay>())
        {
            display.UpdateCounter();
        }
    }

    private void FillCrop(string cropKey, int requiredAmount)
    {
        if (string.IsNullOrEmpty(cropKey)) return;

        // Get current amount of crops in inventory
        int currentAmount = CounterData.GetCounter(cropKey);

        // Check how much we need to fill the shipment requirement
        int amountToAdd = Mathf.Max(0, requiredAmount - currentAmount);

        // If we need to add crops, fill them
        if (amountToAdd > 0)
        {
            CounterData.AddToCounter(cropKey, amountToAdd);
            Debug.Log($"[DEV] Added {amountToAdd} to {CounterData.GetLabel(cropKey)} to reach {requiredAmount}");
        }
        else
        {
            Debug.Log($"[DEV] {CounterData.GetLabel(cropKey)} already has enough: {currentAmount}/{requiredAmount}");
        }
    }
}
