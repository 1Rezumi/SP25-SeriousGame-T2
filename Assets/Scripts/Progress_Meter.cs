using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Meter : MonoBehaviour
{
    public ButtonController buttonController;
    public Button assignedButton;

    public string counterKey = "Crop1";
    public Slider progressSlider;
    public float totalDuration = 10f;

    private float timeElapsed = 0f;
    private bool isFilling = false;

    // Start is called before the first frame update
    void Start()
    {
        if (progressSlider)
        {
            progressSlider.maxValue = totalDuration;
            progressSlider.value = 0; //start empty
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Update running: {counterKey}, timeElapsed: {timeElapsed}");
        if (isFilling)
        {
            //logic for the countdown
            if (timeElapsed < totalDuration)
            {
                timeElapsed += Time.deltaTime;
                if (progressSlider) progressSlider.value = timeElapsed; //Fills up over time
            }
            else
            {
                isFilling = false;

                //Increase the counter in the CounterManager script
                CounterData.AddToCounter(counterKey, 1);
                Debug.Log("Crops are Ready!");

                foreach (CounterDisplay display in FindObjectsOfType<CounterDisplay>())
                {
                    if (display.counterKey == counterKey)
                    {
                        display.UpdateCounter();
                    }
                    Debug.Log("Meter finished for: " + counterKey);
                }

                //Re-enable button after meter completes
                if (buttonController != null && assignedButton != null)
                {
                    buttonController.EnableButton(assignedButton);
                }
            }
        }

;
    }

    public void StartMeter()
    {
        if (!isFilling)
        {
            Debug.Log("StartMeter() called for: " + counterKey);
            timeElapsed = 0f;
            isFilling = true;

            // Disable button when meter starts
            if (assignedButton != null)
            {
                assignedButton.interactable = false;
            }
        }
    }
}
