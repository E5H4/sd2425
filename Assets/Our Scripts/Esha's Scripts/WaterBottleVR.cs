using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class WaterBottleVR : MonoBehaviour
{
    [Header("triggered when the water bottle is picked up")]
    [SerializeField] private DialogueUIController waterBottleDialogueController;
    [SerializeField] private GameObject waterBottleDialogue;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnWaterBottlePickedUp);
        }
        else
        {
            Debug.LogError("Grab Interactable not found on water bottle.");
        }
    }

    private void OnWaterBottlePickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Water bottle picked up!");

        TriggerWaterBottleDialogue();

        // only allows dialogue to go thru ONCE
        grabInteractable.selectEntered.RemoveListener(OnWaterBottlePickedUp);
    }

    private void TriggerWaterBottleDialogue()
    {
        if (waterBottleDialogue != null)
        {
            waterBottleDialogue.SetActive(true);
            Debug.Log("Water bottle dialogue activated.");
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