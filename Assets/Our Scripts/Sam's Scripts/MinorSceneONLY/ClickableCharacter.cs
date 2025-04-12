using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class ClickableCharacterVR : MonoBehaviour
{
    [SerializeField] private GameObject MinorDialogue4;  // Assign in Inspector
    [SerializeField] private GameObject MinorDialogue5;  // Assign in Inspector
    [SerializeField] private DialogueUIController minordialogue5controller;

    // Reference to the XR Interactable component for VR interaction.
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        // Attempt to find an XR interactable component on this object.
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("ClickableCharacterVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to VR selection; this fires when the user "clicks" the object.
            interactable.selectEntered.AddListener(OnCharacterSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnCharacterSelected);
        }
    }

    // Called when the character is selected by the VR controller.
    private void OnCharacterSelected(SelectEnterEventArgs args)
    {
        Debug.Log("ClickableCharacterVR: Character selected via VR!");

        // Check the static condition from Bookbag.
        if (Bookbag.soda)
        {
            Debug.Log("ClickableCharacterVR: Soda is true! Switching dialogues.");

            // Disable MinorDialogue4 if it is active.
            if (MinorDialogue4 != null && MinorDialogue4.activeSelf)
            {
                MinorDialogue4.SetActive(false);
                Debug.Log("ClickableCharacterVR: MinorDialogue4 turned off.");
            }

            // Enable MinorDialogue5 and trigger the dialogue.
            if (MinorDialogue5 != null)
            {
                MinorDialogue5.SetActive(true);
                Debug.Log("ClickableCharacterVR: MinorDialogue5 activated!");
                if (minordialogue5controller != null)
                {
                    minordialogue5controller.ShowDialogueUI();
                }
            }
        }
        else
        {
            Debug.Log("ClickableCharacterVR: Soda is not true yet, cannot proceed.");
        }
    }
}