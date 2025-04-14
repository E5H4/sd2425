using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class SodaVRCollisionTrigger : MonoBehaviour
{
    [Header("Dialogue Controllers")]
    [Tooltip("The DialogueUIController for MinorDialogue4 (this will be turned off)")]
    [SerializeField] private DialogueUIController minorDialogue4Controller;
    [Tooltip("The DialogueUIController for MinorDialogue5 (this will be triggered)")]
    [SerializeField] private DialogueUIController minorDialogue5Controller;

    // Reference to the XRGrabInteractable component on this object.
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    // Flag to ensure the trigger only happens once.
    private bool hasTriggered = false;

    // Flag to track when this object is currently held.
    private bool isHeld = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("SodaVRCollisionTrigger: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("SodaVRCollisionTrigger: Object grabbed.");
        minorDialogue4Controller.ShowDialogueUI();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
        Debug.Log("SodaVRCollisionTrigger: Object released.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"SodaVRCollisionTrigger: Trigger entered by {other.gameObject.name} with tag {other.gameObject.tag} on layer {LayerMask.LayerToName(other.gameObject.layer)}.");
        if (hasTriggered)
            return;

        // Check if the collided object is tagged "Character" and this object is currently being held.
        if (other.CompareTag("Character") && isHeld)
        {
            Debug.Log("SodaVRCollisionTrigger: Collided with character while held.");

            // Turn off MinorDialogue4.
            if (minorDialogue4Controller != null)
            {
                minorDialogue4Controller.gameObject.SetActive(false);
            }

            // Activate and show MinorDialogue5.
            if (minorDialogue5Controller != null)
            {
                minorDialogue5Controller.gameObject.SetActive(true);
                minorDialogue5Controller.ShowDialogueUI();
            }

            hasTriggered = true;

            // Optionally, disable this object so it is no longer visible.
            gameObject.SetActive(false);
        }
    }
}