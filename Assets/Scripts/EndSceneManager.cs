using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void OnReturnToTitlePressed()
    {
        CounterData.ResetAll(); // resets counters & cooldowns
        SceneManager.LoadScene("SampleScene"); // Update name as needed
    }

    void ResetAllGameData()
    {
        CounterData.ResetAll();
        CounterData.ClearCooldowns();
    }
}