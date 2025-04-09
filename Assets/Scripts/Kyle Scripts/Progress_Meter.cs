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
    private int counter = 0; // Tracks how many times the meter has completed

    // Start is called before the first frame update
    void Start()
    {
        if (progressSlider)
        {
            progressSlider.maxValue = totalDuration;
            progressSlider.value = 0; //start empty
        }
        enabled = false; // initially disables this script
    }

    // Update is called once per frame
    void Update()
    {
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
                enabled = false; // Disable this script when finished

                //Increase the counter in the CounterManager script
                CounterData.IncreaseCounter(counterKey);
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
            timeElapsed = 0f;
            isFilling = true;
            enabled = true; // Enable the script when starting

            // Disable button when meter starts
            if (assignedButton != null)
            {
                assignedButton.interactable = false;
            }
        }
    }
}
