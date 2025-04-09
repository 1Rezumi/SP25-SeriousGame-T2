using UnityEngine;
using UnityEngine.UI;

public class TimerButtonControl : MonoBehaviour
{
    public Image progressImage;  // Assign if using an Image Fill
    public Slider progressSlider; // Assign if using a Slider
    public Button actionButton;   // Assign your button in the Inspector
    public float countdownTime = 10f; 

    private float timeElapsed;
    private bool isFilling = false;

    void Start()
    {
        // Ensure the meter starts empty
        if (progressImage) progressImage.fillAmount = 0;
        if (progressSlider)
        {
            progressSlider.value = 0;
            progressSlider.maxValue = countdownTime;
        }

        actionButton.onClick.AddListener(StartMeter); // Attach button functionality
    }

    void Update()
    {
        if (isFilling)
        {
            if (timeElapsed < countdownTime)
            {
                timeElapsed += Time.deltaTime;
                float progress = timeElapsed / countdownTime;

                // Update meter
                if (progressImage) progressImage.fillAmount = progress;
                if (progressSlider) progressSlider.value = timeElapsed;
            }
            else
            {
                isFilling = false;
                actionButton.interactable = true; // Re-enable button after filling
            }
        }
    }

    void StartMeter()
    {
        if (!isFilling)
        {
            timeElapsed = 0;
            isFilling = true;
            actionButton.interactable = false; // Disable button during fill
        }
    }
}
