using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventButton : MonoBehaviour
{
    public Button eventButton;
    public float cooldownDuration = 25f;
    public EventMessageDisplay messageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (eventButton != null)
        {
            eventButton.onClick.AddListener(TriggerRandomEvent);
        }
        float elapsed = Time.time - CounterData.GetLastEventTime();
        if (elapsed < cooldownDuration)
        {
            eventButton.interactable = false;
            StartCoroutine(CooldownCoroutine(cooldownDuration - elapsed));
        }
    }

    void TriggerRandomEvent()
    {
        CounterData.SetLastEventTime(Time.time); // store time of activation
        eventButton.interactable = false; //disable the button when going on cooldown
        StartCoroutine(CooldownCoroutine(cooldownDuration));

        List<string> keys = CounterData.GetAllKeys();
        if (keys.Count == 0) return;

        int roll = Random.Range(0, 100);

        if (roll < 15)
        {
            //Rare Event: Reset all crops to 0
            foreach (string key in keys)
                CounterData.SetCounter(key, 0);
            messageDisplay.ShowMessage("Event: All crops reset to 0!");
        }
        else if (roll < 30)
        {
            // Rare Event: Double all crops
            foreach (string key in keys)
                CounterData.SetCounter(key, CounterData.GetCounter(key) * 2);
            messageDisplay.ShowMessage("Event: All crops doubled!");
        }
        else
        {
            // Common Events
            int effect = Random.Range(0, 5);
            string randomKey = keys[Random.Range(0, keys.Count)];
            int current;

            switch (effect)
            {
                case 0: // +3 to all crops
                    foreach (string key in keys)
                        CounterData.AddToCounter(key, 3);
                    messageDisplay.ShowMessage("Event: +3 to all crops");
                    break;
                case 1: // -2 to all crops
                    foreach (string key in keys)
                        CounterData.AddToCounter(key, -2);
                    messageDisplay.ShowMessage("Event: -2 from all crops");
                    break;
                case 2: //+7 to random crop
                    CounterData.AddToCounter(randomKey, 7);
                    string friendlyName = CounterData.GetLabel(randomKey);
                    messageDisplay.ShowMessage("Event: +7 to " + friendlyName);
                    break;
                case 3: // Reset a random crop
                    CounterData.SetCounter(randomKey, 0);
                    messageDisplay.ShowMessage("Event: " + CounterData.GetLabel(randomKey) + " reset to 0");
                    break;
                case 4: // Double a random crop
                    current = CounterData.GetCounter(randomKey);
                    CounterData.SetCounter(randomKey, current * 2);
                    messageDisplay.ShowMessage("Event: " + CounterData.GetLabel(randomKey) + " doubled");
                    break;


            }
        }
            // Update Displays
            foreach (CounterDisplay display in FindObjectsOfType<CounterDisplay>())
            {
                display.UpdateCounter();
            }
        }

        IEnumerator CooldownCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            eventButton.interactable = true;
        }
    }
