using UnityEngine;
using DialogueSystemWithText;

public class VileSyringeShakable : MonoBehaviour
{
    // Dialogue to trigger once the shaking is complete.
    [SerializeField] private DialogueUIController severeDialogue8;
    // How many shake extremes need to be reached (each extreme change counts).
    [SerializeField] private int requiredShakeCount = 5;
    // Allowed horizontal range from the initial position.
    [SerializeField] private float shakeRange = 0.15f;
    // Tolerance for detecting an extreme (in local x).
    [SerializeField] private float extremeTolerance = 0.02f;

    private int shakeCount = 0;
    private float initialLocalX;
    // Keeps track of which extreme was last reached: "left", "right", or "none".
    private string lastExtreme = "none";

    // This flag lets you enable/disable shake detection.
    private bool shakeEnabled = false;

    // Call this function externally to start shake detection.
    public void EnableShakeMode()
    {
        // Record the starting local x.
        initialLocalX = transform.localPosition.x;
        shakeCount = 0;
        lastExtreme = "none";
        shakeEnabled = true;
        Debug.Log("VileSyringeShakable: Shake mode enabled.");
    }

    private void Update()
    {
        if (!shakeEnabled) return;

        // Get the current local x relative to initial.
        float currentX = transform.localPosition.x;
        float offset = currentX - initialLocalX;

        // Check if we've reached the left extreme.
        if (offset <= -shakeRange + extremeTolerance)
        {
            if (lastExtreme != "left")
            {
                lastExtreme = "left";
                shakeCount++;
                Debug.Log("VileSyringeShakable: Left extreme reached. Shake count: " + shakeCount);
            }
        }
        // Check if we've reached the right extreme.
        else if (offset >= shakeRange - extremeTolerance)
        {
            if (lastExtreme != "right")
            {
                lastExtreme = "right";
                shakeCount++;
                Debug.Log("VileSyringeShakable: Right extreme reached. Shake count: " + shakeCount);
            }
        }
        // If the object returns near the center, reset lastExtreme so a new extreme can be counted.
        else if (Mathf.Abs(offset) < extremeTolerance)
        {
            lastExtreme = "none";
        }

        if (shakeCount >= requiredShakeCount)
        {
            Debug.Log("VileSyringeShakable: Required shake count reached. Triggering dialogue.");
            if (severeDialogueShake != null)
            {
                severeDialogueShake.ShowDialogueUI();
            }
            shakeEnabled = false;
        }
    }
}