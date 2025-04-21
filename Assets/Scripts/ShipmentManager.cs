using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Shipment
{
    public string cropKey1;
    public int requiredAmount1;
    public TextMeshProUGUI progressText1;

    public string cropKey2;
    public int requiredAmount2;
    public TextMeshProUGUI progressText2;

    public TextMeshProUGUI completionText;

    public bool IsCompleted(int index)
    {
        (int p1, int p2) = CounterData.GetShipmentProgress(index);
        return p1 >= requiredAmount1 && p2 >= requiredAmount2;
    }

    public bool ShipmentSent => completionText != null && completionText.text == "1/1";
}

public class ShipmentManager : MonoBehaviour
{
    public List<Shipment> shipments;

    private void Awake()
    {
        SetDefaultLabels();
    }

    private void Start()
    {
        StartCoroutine(DelayedUIUpdate());
    }

    private IEnumerator DelayedUIUpdate()
    {
        yield return null;
        UpdateAllUI();
    }

    private void SetDefaultLabels()
    {
        CounterData.SetLabel("Crop1", "Speets");
        CounterData.SetLabel("Crop2", "Skelp");
        CounterData.SetLabel("Crop3", "Brussel");
    }

    public void TrySubmitShipment(int index)
    {
        if (index < 0 || index >= shipments.Count) return;

        Shipment shipment = shipments[index];
        if (shipment.IsCompleted(index) || shipment.ShipmentSent) return;

        int available1 = CounterData.GetCounter(shipment.cropKey1);
        int available2 = CounterData.GetCounter(shipment.cropKey2);

        (int current1, int current2) = CounterData.GetShipmentProgress(index);

        int needed1 = Mathf.Max(0, shipment.requiredAmount1 - current1);
        int needed2 = Mathf.Max(0, shipment.requiredAmount2 - current2);

        int take1 = Mathf.Min(available1, needed1);
        int take2 = Mathf.Min(available2, needed2);

        int newProgress1 = Mathf.Min(current1 + take1, shipment.requiredAmount1);
        int newProgress2 = Mathf.Min(current2 + take2, shipment.requiredAmount2);

        CounterData.AddToCounter(shipment.cropKey1, -take1);
        CounterData.AddToCounter(shipment.cropKey2, -take2);

        CounterData.SetShipmentProgress(index, newProgress1, newProgress2);

        UpdateShipmentUI(index, shipment);

        if (shipment.IsCompleted(index))
        {
            CounterData.MarkShipmentCompleted(index);
            shipment.completionText.text = "<color=#00FF00>1/1</color>";
        }

        if (AllShipmentsCompleted())
        {
            Debug.Log("All shipments complete! Ending game.");
            StartCoroutine(EndGameSequence());
        }

        CounterData.CounterDisplayUpdater.RefreshAllDisplays();
        UpdateAllUI();
    }

    public void DevFillShipments()
    {
        foreach (var shipment in shipments)
        {
            int available1 = CounterData.GetCounter(shipment.cropKey1);
            int available2 = CounterData.GetCounter(shipment.cropKey2);

            int toAdd1 = Mathf.Max(0, shipment.requiredAmount1 - available1);
            int toAdd2 = Mathf.Max(0, shipment.requiredAmount2 - available2);

            CounterData.AddToCounter(shipment.cropKey1, toAdd1);
            CounterData.AddToCounter(shipment.cropKey2, toAdd2);
        }

        CounterData.CounterDisplayUpdater.RefreshAllDisplays();
    }

    void UpdateShipmentUI(int index, Shipment shipment)
    {
        string label1 = CounterData.GetLabel(shipment.cropKey1);
        string label2 = CounterData.GetLabel(shipment.cropKey2);

        (int progress1, int progress2) = CounterData.GetShipmentProgress(index);

        int clamped1 = Mathf.Min(progress1, shipment.requiredAmount1);
        int clamped2 = Mathf.Min(progress2, shipment.requiredAmount2);

        bool enough1 = clamped1 >= shipment.requiredAmount1;
        bool enough2 = clamped2 >= shipment.requiredAmount2;

        string color1 = enough1 ? "#00FF00" : "#FFFFFF";
        string color2 = enough2 ? "#00FF00" : "#FFFFFF";

        shipment.progressText1.text = $"{label1} <color={color1}>{clamped1}/{shipment.requiredAmount1}</color>";
        shipment.progressText2.text = $"{label2} <color={color2}>{clamped2}/{shipment.requiredAmount2}</color>";

        if (CounterData.IsShipmentMarkedCompleted(index))
        {
            shipment.completionText.text = "<color=#00FF00>1/1</color>";
        }
        else
        {
            shipment.completionText.text = "0/1";
        }
    }


    void UpdateAllUI()
    {
        for (int i = 0; i < shipments.Count; i++)
        {
            UpdateShipmentUI(i, shipments[i]);
            shipments[i].completionText.text = shipments[i].IsCompleted(i) ? "<color=#00FF00>1/1</color>" : "0/1";

        }
    }

    bool AllShipmentsCompleted()
    {
        for (int i = 0; i < shipments.Count; i++)
        {
            if (!shipments[i].IsCompleted(i)) return false;
        }
        return true;
    }

    IEnumerator EndGameSequence()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene");
    }
}
