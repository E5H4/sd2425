using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class PhoneInteraction : MonoBehaviour
{
    [Header("Triggered when the phone is selected/grabbed")]
    [SerializeField] private DialogueUIController Called911PhoneDialogueController;  // Reference to the DialogueUIController
    [SerializeField] private GameObject phoneDialogue;            // Reference to the Dialogue UI GameObject

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;

    // Set up for triggering
    private void Awake()
    {
        simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnPhonePickedUp);
        }
        else
        {
            Debug.LogError("No XRSimpleInteractable component found on the phone!");
        }
    }

    // Called when the phone is selected (picked up)
    private void OnPhonePickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Phone selected!");

        // Trigger the dialogue when the phone is selected
        TriggerPhoneDialogue();
        
        // Prevent repeat triggering
        simpleInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
    }

    // Method to trigger the Phone dialogue UI
    private void TriggerPhoneDialogue()
    {
        if (phoneDialogue != null)
        {
            phoneDialogue.SetActive(true);
            Debug.Log("Phone dialogue activated.");
        }

        if (Called911PhoneDialogueController != null)
        {
            Called911PhoneDialogueController.ShowDialogueUI();
        }
    }

    private void OnDestroy()
    {
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
        }
    }
}