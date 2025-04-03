using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePlantScene : MonoBehaviour
{

    public void goToPlantScene()
    {
        SceneManager.LoadScene("PlantsScene");
    }

}

