using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class PlungerShotDraggableVR : MonoBehaviour
{
    [Header("Dialogue References")]
    [Tooltip("Dialogue controller to turn off (Dialogue 10).")]
    [SerializeField] private DialogueUIController severeDialogue10;
    [Tooltip("Dialogue controller to trigger (Dialogue 11).")]
    [SerializeField] private DialogueUIController severeDialogue11;

    [Header("Draggable Settings")]
    [Tooltip("How far (in local Y units) the plunger must be pushed for the trigger to fire.")]
    [SerializeField] private float dragDistanceThreshold = 0.06f;
    [Tooltip("Tolerance for triggering dialogue (in local Y units).")]
    [SerializeField] private float triggerTolerance = 0.001f;

    // Internal values to constrain movement.
    private float fixedLocalX;
    private float fixedLocalZ;
    private float initialLocalY;

    // Reference to the XRGrabInteractable component
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Record the current local X and Z so that they remain fixed.
        Vector3 localPos = transform.localPosition;
        fixedLocalX = localPos.x;
        fixedLocalZ = localPos.z;
        // initialLocalY will be set when grabbed.
        initialLocalY = localPos.y;

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("PlungerShotDraggableVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    // Called when the object is grabbed.
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Record the initial Y position when grabbed.
        initialLocalY = transform.localPosition.y;
        Debug.Log($"PlungerShotDraggableVR: Grabbed. initialLocalY = {initialLocalY}");
    }

    private void Update()
    {
        // If the object is being held, force its position to only change in Y.
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            // Get current local Y from transform.
            float currentY = transform.localPosition.y;
            // Prevent the plunger from moving upward beyond its initial position.
            float clampedY = Mathf.Max(initialLocalY, currentY);
            // Also clamp to the maximum allowed push-down value.
            clampedY = Mathf.Min(clampedY, initialLocalY + dragDistanceThreshold);

            // Set the position with fixed X and Z.
            transform.localPosition = new Vector3(fixedLocalX, clampedY, fixedLocalZ);
        }
    }

    // Called when the object is released.
    private void OnReleased(SelectExitEventArgs args)
    {
        float currentLocalY = transform.localPosition.y;
        float movedDistance = currentLocalY - initialLocalY;
        Debug.Log($"PlungerShotDraggableVR: Released. Current local Y = {currentLocalY}, moved distance = {movedDistance}");

        // Check if the moved distance meets (or nearly meets) the threshold.
        if (movedDistance >= dragDistanceThreshold - triggerTolerance)
        {
            Debug.Log("PlungerShotDraggableVR: Drag threshold reached. Triggering Dialogue 11.");
            // Turn off Dialogue 10.
            if (severeDialogue10 != null)
                severeDialogue10.gameObject.SetActive(false);
            // Activate and show Dialogue 11.
            if (severeDialogue11 != null)
            {
                severeDialogue11.gameObject.SetActive(true);
                severeDialogue11.ShowDialogueUI();
            }
        }
        else
        {
            Debug.Log("PlungerShotDraggableVR: Drag threshold not met. No dialogue triggered.");
        }
    }
}