using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterDisplay : MonoBehaviour
{
    public string counterKey = "Crop1";
    public TMP_Text counterText;

    void Start()
    {
        UpdateCounter();
    }


    void OnEnable()
    {
        UpdateCounter();
    }
    // Update is called once per frame
    public void UpdateCounter()
    {
        if (counterText != null && !string.IsNullOrEmpty(counterKey))
        {
            counterText.text = "Crops: " + CounterData.GetCounter(counterKey);
        }
    }
}
