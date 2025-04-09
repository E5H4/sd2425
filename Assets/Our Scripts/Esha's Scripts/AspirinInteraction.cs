using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class AspirinInteraction : MonoBehaviour
{
    [Header("triggered when the aspirin touches the victim")]
    [SerializeField] private DialogueUIController aspirinCheckVRController;  
    [SerializeField] private GameObject aspirinCheckVR;            
    [SerializeField] private GameObject assignedVictim;                 // Reference to the character to check for collision

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    // Set up for triggering
    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnAspirinPickedUp);
        }
    }

    // Called when the aspirin is picked up
    private void OnAspirinPickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Aspirin picked up!");

        // Trigger the dialogue when picked up
        TriggerAspirinCheckDialogue();
        
        // Prevent repeat triggering
        grabInteractable.selectEntered.RemoveListener(OnAspirinPickedUp);
    }

    // Called when the aspirin collides with the assigned character
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the assigned character
        if (other.gameObject == assignedVictim)
        {
            Debug.Log("Aspirin touched the assigned character!");

            // Trigger the dialogue when the aspirin touches the character
            TriggerAspirinCheckDialogue();
        }
    }

    // Method to trigger the AspirinCheckVR dialogue UI
    private void TriggerAspirinCheckDialogue()
    {
        if (aspirinCheckVR != null)
        {
            aspirinCheckVR.SetActive(true);
            Debug.Log("AspirinCheckVR dialogue activated.");
        }

        if (aspirinCheckVRController != null)
        {
            aspirinCheckVRController.ShowDialogueUI();
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnAspirinPickedUp);
        }
    }
}