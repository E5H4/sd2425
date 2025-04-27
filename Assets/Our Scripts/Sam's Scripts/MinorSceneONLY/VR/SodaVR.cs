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
    [SerializeField] private DialogueUIController Dialogue1;
    [SerializeField] private DialogueUIController Dialogue2;
    [SerializeField] private DialogueUIController Dialogue3;
    [SerializeField] private DialogueUIController Dialogue4;
    [SerializeField] private DialogueUIController Dialogue5;
    [SerializeField] private DialogueUIController Dialogue6;

    [SerializeField] private GameObject bag;


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
        DeactivateAllDialogues();
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
            minorDialogue4Controller.HideDialogueUI();
            Debug.Log("Dialogue Turned Off");

            // Activate and show MinorDialogue5.
            if (minorDialogue5Controller != null)
            {
                minorDialogue5Controller.HideDialogueUI();
                minorDialogue5Controller.ShowDialogueUI();
                bag.SetActive(false);
            }

            hasTriggered = true;

            // Optionally, disable this object so it is no longer visible.
            gameObject.SetActive(false);
        }
    }

    public void DeactivateAllDialogues()
    {
        Dialogue1?.HideDialogueUI();
        Dialogue2?.HideDialogueUI();
        Dialogue3?.HideDialogueUI();
        Dialogue4?.HideDialogueUI();
        Dialogue5?.HideDialogueUI();
        Dialogue6?.HideDialogueUI();
    }
}