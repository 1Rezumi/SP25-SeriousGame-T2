using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventMessageDisplay : MonoBehaviour
{

    public TMP_Text messageText;
    public float displayDuration = 3f;

    private Coroutine currentRoutine;

    public void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(DisplayRoutine(message));
    }

    IEnumerator DisplayRoutine(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        messageText.gameObject.SetActive(false);
    }
}
