using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [System.Serializable]
    public class ButtonMeterPair
    {
        public Progress_Meter meterScript; // reference meter script
        public Button actionButton;
    }

    public List<ButtonMeterPair> buttonMeterPairs = new List<ButtonMeterPair>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pair in buttonMeterPairs)
        {
            if (pair.meterScript != null)
                pair.meterScript.enabled = false; // Disable meter script at beginning

            if (pair.actionButton != null)
                pair.actionButton.onClick.AddListener(() => StartMeter(pair));
        }
       
    }

    // Update is called once per frame
    public void StartMeter(ButtonMeterPair pair)
    {
        if (pair.actionButton != null)
        pair.actionButton.interactable = false; // disable the button

        if (pair.meterScript != null)
        {
            pair.meterScript.enabled = true; // Enable meter script.
            pair.meterScript.StartMeter();
            pair.meterScript.buttonController = this;
        }

        

    }
    public void EnableButton(Button actionButton)
    {
        if (actionButton != null)
        {
            actionButton.interactable = true;
        }
            
    }
}
