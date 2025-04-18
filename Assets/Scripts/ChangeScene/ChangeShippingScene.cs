using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeShippingScene : MonoBehaviour
{

    public void goToShippingScene()
    {
        SceneManager.LoadScene("ShippingScene");
    }

}
