using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePlanetScene : MonoBehaviour
{

    public void goToPlanetScene()
    {
        SceneManager.LoadScene("PlanetsScene");
    }

}
