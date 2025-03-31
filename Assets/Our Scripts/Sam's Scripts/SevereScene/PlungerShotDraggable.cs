using UnityEngine;
using DialogueSystemWithText;

public class PlungerShotDraggable : MonoBehaviour
{
    // Dialogue to turn off (Dialogue 11)
    [SerializeField] private DialogueUIController severeDialogue11;
    // Dialogue to trigger (Dialogue 12)
    [SerializeField] private DialogueUIController severeDialogue12;

    // How far (in local Y units) the plunger can be moved.
    // For example, if the plunger starts at -0.086 and should move to -0.086 + 0.06 = -0.026.
    [SerializeField] private float dragDistanceThreshold = 0.06f;
    // Tolerance for triggering dialogue (in local Y units)
    [SerializeField] private float triggerTolerance = 0.001f;

    // Variables for tracking dragging.
    private bool isDragging = false;
    private float initialMouseY;
    private float initialLocalY;
    // We'll keep x and z fixed.
    private float fixedLocalX;
    private float fixedLocalZ;

    private void Start()
    {
        // Record the starting fixed x and z positions.
        Vector3 localPos = transform.localPosition;
        fixedLocalX = localPos.x;
        fixedLocalZ = localPos.z;
        initialLocalY = localPos.y;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        // Record initial mouse Y (screen space) and current local Y.
        initialMouseY = Input.mousePosition.y;
        initialLocalY = transform.localPosition.y;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
            return;

        float currentMouseY = Input.mousePosition.y;
        // Calculate the mouse delta. When dragging down, currentMouseY is lower than initialMouseY,
        // so (initialMouseY - currentMouseY) is positive.
        float deltaMouse = initialMouseY - currentMouseY;
        // Multiply by a scale factor (adjust as needed).
        float dragScale = 0.001f;
        float deltaLocalY = deltaMouse * dragScale;
        // Compute the new local Y. We add the delta so that the plunger's local Y increases (becomes less negative).
        float newLocalY = initialLocalY + deltaLocalY;
        // Clamp newLocalY between the starting position and the starting position plus the threshold.
        newLocalY = Mathf.Clamp(newLocalY, initialLocalY, initialLocalY + dragDistanceThreshold);
        transform.localPosition = new Vector3(fixedLocalX, newLocalY, fixedLocalZ);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        // Calculate how far the plunger has been moved.
        float movedDistance = transform.localPosition.y - initialLocalY;
        if (movedDistance >= dragDistanceThreshold - triggerTolerance)
        {
            Debug.Log("PlungerShotDraggable: Drag threshold reached (" + movedDistance + " units). Triggering Dialogue 12.");
            // Turn off Dialogue 11.
            if (severeDialogue11 != null)
                severeDialogue11.gameObject.SetActive(false);
            // Activate and show Dialogue 12.
            if (severeDialogue12 != null)
            {
                severeDialogue12.gameObject.SetActive(true);
                severeDialogue12.ShowDialogueUI();
            }
        }
    }
}