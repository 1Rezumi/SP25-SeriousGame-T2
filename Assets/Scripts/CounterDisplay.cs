using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterDisplay : MonoBehaviour
{
    public string counterKey = "Crop1";
    public string label = "Crops: "; // customizable in the inspector
    public TMP_Text counterText;

    void Start()
    {
        if (!CounterData.GetAllKeys().Contains(counterKey))
        {
            CounterData.SetCounter(counterKey, 0); // Only if you really want to force init
        }
        CounterData.SetLabel(counterKey, label);
        UpdateCounter();
    }


    void OnEnable()
    {
        CounterData.EnsureCounterExists(counterKey);
        UpdateCounter();
    }
    // Update is called once per frame
    public void UpdateCounter()
    {
        if (counterText != null && !string.IsNullOrEmpty(counterKey))
        {
            counterText.text = label + CounterData.GetCounter(counterKey);
        }
    }
}
