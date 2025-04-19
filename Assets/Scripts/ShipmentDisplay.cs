using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ShipmentDisplay : MonoBehaviour
{
    public TMP_Text crop1Text;
    public TMP_Text crop2Text;
    public string cropKey1;
    public string cropKey2;
    public int requiredAmount1;
    public int requiredAmount2;

    public void UpdateDisplay()
    {
        int current1 = CounterData.GetCounter(cropKey1);
        int current2 = CounterData.GetCounter(cropKey2);

        string name1 = CounterData.GetLabel(cropKey1);
        string name2 = CounterData.GetLabel(cropKey2);

        crop1Text.text = $"{name1}: ({Mathf.Min(current1, requiredAmount1)}/{requiredAmount1})";
        crop2Text.text = $"{name2}: ({Mathf.Min(current2, requiredAmount2)}/{requiredAmount2})";
    }
}