using UnityEngine;

public class DevShipmentFiller : MonoBehaviour
{
    // this whole scirpt is used to test the shipment scene, it won't be used in the final product.

    [Header("Crop Requirements for Shipment")]
    public string cropKey1;
    public int requiredAmount1;

    public string cropKey2;
    public int requiredAmount2;

    public string cropKey3;
    public int requiredAmount3;

    public void FillCropsForShipment()
    {
        FillCrop(cropKey1, requiredAmount1);
        FillCrop(cropKey2, requiredAmount2);
        FillCrop(cropKey3, requiredAmount3);

        foreach (var display in FindObjectsOfType<CounterDisplay>())
        {
            display.UpdateCounter();
        }
    }



    private void FillCrop(string cropKey, int requiredAmount)
    {
        if (string.IsNullOrEmpty(cropKey)) return;

        int currentAmount = CounterData.GetCounter(cropKey);

        int amountToAdd = Mathf.Max(0, requiredAmount - currentAmount);

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
