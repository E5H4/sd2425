using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonMasher : MonoBehaviour
{
    /*
     public float pressureLevel = 0f; // Current pressure level
    public float pressureIncreaseAmount = 5f; // Amount added per button mash
    public float requiredPressureLevel = 80f; // Pressure needed to continue
    public float timeLimit = 20f; // Time limit in seconds
    public Renderer legRenderer;
    public Slider pressureBarUI; // Reference to the UI progress bar
    public Text timerTextUI; //  UI Text inside the progress bar

    private bool hasMetPressureRequirement = false;
    private bool hasFailed = false;
    private float timer = 0f;

    void Start()
    {
        timer = timeLimit;

        if (pressureBarUI != null)
        {
            pressureBarUI.minValue = 0f;
            pressureBarUI.maxValue = requiredPressureLevel;
            pressureBarUI.value = pressureLevel;
        }

        UpdateTimerUI();
    }

    void Update()
    {
        if (hasMetPressureRequirement || hasFailed)
            return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        // Replace keyboard input with VR trigger input
        if (Input.GetButtonDown("Trigger")) //  Requires InputManager setup or new Input System
        {
            pressureLevel += pressureIncreaseAmount;
            pressureLevel = Mathf.Clamp(pressureLevel, 0f, 100f);

            if (pressureBarUI != null)
                pressureBarUI.value = pressureLevel;
        }

        ApplyVisualPressure();

        if (pressureLevel >= requiredPressureLevel)
        {
            hasMetPressureRequirement = true;
            ProceedToNextSection();
        }
        else if (timer <= 0f)
        {
            hasFailed = true;
            HandleFailure();
        }
    }

    void UpdateTimerUI()
    {
        if (timerTextUI != null)
        {
            float timeRemaining = Mathf.Max(0f, timer);
            timerTextUI.text = $"Time: {timeRemaining:F1}s";
        }
    }

    void ApplyVisualPressure()
    {
        float redness = Mathf.Clamp01(pressureLevel / 100f);

        if (hasMetPressureRequirement)
        {
            legRenderer.material.color = Color.green;
        }
        else if (hasFailed)
        {
            legRenderer.material.color = Color.black;
        }
        else
        {
            legRenderer.material.color = Color.Lerp(Color.white, Color.red, redness);
        }
    }

    void ProceedToNextSection()
    {
        Debug.Log("Pressure threshold met! Proceeding to the next section...");
    }

    void HandleFailure()
    {
        Debug.Log("Time's up! Failed to meet the required pressure.");
    }
    */

}
