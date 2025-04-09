using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowthController : MonoBehaviour
{
    public GameObject menuPanel;

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
