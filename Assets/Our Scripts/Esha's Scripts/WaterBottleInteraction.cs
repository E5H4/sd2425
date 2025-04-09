using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class WaterBottleInteraction : MonoBehaviour //used later lol
{
    [Header("triggered when the water bottle touches the victim")]
    [SerializeField] private DialogueUIController waterBottleDialogueController;  
    [SerializeField] private GameObject waterBottleDialogue;          
    [SerializeField] private GameObject assignedVictim;                

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    // Set up for triggering
    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnWaterBottlePickedUp);
        }
    }

    // Called when the water bottle is picked up
    private void OnWaterBottlePickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Water bottle picked up!");

        
        TriggerWaterBottleDialogue();
        
        
        grabInteractable.selectEntered.RemoveListener(OnWaterBottlePickedUp);
    }

    // Called when the water bottle collides with the assigned character
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == assignedVictim)
        {
            Debug.Log("Water bottle touched the assigned victim!");

            TriggerWaterBottleDialogue();
        }
    }

    // Method to trigger the Water Bottle dialogue UI
    private void TriggerWaterBottleDialogue()
    {
        if (waterBottleDialogue != null)
        {
            waterBottleDialogue.SetActive(true);
            Debug.Log("WaterBottleDialogue activated.");
        }

        if (waterBottleDialogueController != null)
        {
            waterBottleDialogueController.ShowDialogueUI();
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnWaterBottlePickedUp);
        }
    }
}