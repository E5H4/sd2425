using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

[RequireComponent(typeof(Collider))]
public class PlungerDraggableVR_Triggers : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip("Dialogue controller to trigger when push‑in trigger is reached.")]
    [SerializeField] private DialogueUIController severeDialogue7;
    [Tooltip("Dialogue object for push‑in dialogue.")]
    [SerializeField] private GameObject severedialogue7;

    [Tooltip("Dialogue controller to trigger when pull‑out trigger is reached.")]
    [SerializeField] private DialogueUIController severeDialogue10;
    [Tooltip("Dialogue object for pull‑out dialogue.")]
    [SerializeField] private GameObject severedialogue10;

    [Header("Mode Settings")]
    [Tooltip("False = push‑in mode (default), true = pull‑out mode.")]
    [SerializeField] private bool pullOutMode = false;

    // This flag prevents multiple triggers.
    private bool dialogueTriggered = false;

    // Reference to the XR Grab Interactable component on the plunger.
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("PlungerDraggableVR_Triggers: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        // Subscribe to XR events.
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    // Optional: Reset the dialogue trigger flag when the plunger is grabbed.
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        dialogueTriggered = false;
        Debug.Log("PlungerDraggableVR_Triggers: Plunger grabbed; resetting dialogue trigger.");
    }

    // This event is triggered when the player releases the plunger.
    // We assume the triggers will fire OnTriggerEnter if the plunger is in the correct zone.
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("PlungerDraggableVR_Triggers: Plunger released.");
        // No direct checks here. The trigger colliders will handle the dialogue.
    }

    // When the plunger enters one of the trigger colliders (set as children or separate objects in the scene)
    private void OnTriggerEnter(Collider other)
    {
        // Make sure this triggers only once per grab-release cycle.
        if (dialogueTriggered)
            return;

        // Use tags on the trigger colliders for identification.
        if (other.CompareTag("PlungerDown") && !pullOutMode)
        {
            // In push‑in mode, if the plunger enters the downward trigger, trigger Dialogue 7.
            Debug.Log("PlungerDraggableVR_Triggers: Down trigger activated. Triggering Dialogue 7.");
            if (severedialogue7 != null)
            {
                severedialogue7.SetActive(true);
            }
            if (severeDialogue7 != null)
            {
                severeDialogue7.ShowDialogueUI();
            }
            dialogueTriggered = true;
            pullOutMode = true; // Switch mode for future interactions.
        }
        else if (other.CompareTag("PlungerUp") && pullOutMode)
        {
            // In pull‑out mode, if the plunger enters the upward trigger, trigger Dialogue 10.
            Debug.Log("PlungerDraggableVR_Triggers: Up trigger activated. Triggering Dialogue 10.");
            if (severedialogue10 != null)
            {
                severedialogue10.SetActive(true);
            }
            if (severeDialogue10 != null)
            {
                severeDialogue10.ShowDialogueUI();
            }
            dialogueTriggered = true;
        }
    }
}