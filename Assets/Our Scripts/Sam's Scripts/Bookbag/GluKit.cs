using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class Kit : MonoBehaviour
{
    [SerializeField] private DialogueUIController KitDialoguecontroller;
    [SerializeField] private GameObject KitDialogue;
    [SerializeField] private DialogueUIController SevereDialogue2controller;
    [SerializeField] private GameObject SevereDialogue2;
    [SerializeField] private GameObject bag;


    // Reference to the XR Interactable component 
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("Obj: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event
            interactable.selectEntered.AddListener(OnObjSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnObjSelected);
        }
    }

    // This is called when the backpack is activated via VR
    private void OnObjSelected(SelectEnterEventArgs args)
    {
        if (Difficulty.difficulty == "Minor")
        {
            if (KitDialogue != null)
            {
                KitDialogue.SetActive(true);
            }

            if (KitDialoguecontroller != null)
            {
                KitDialoguecontroller.ShowDialogueUI();
            }
        }
        else
        {
            if (bag.activeInHierarchy)
            {
                bag.SetActive(false);
            }

            if (SevereDialogue2 != null)
            {
                SevereDialogue2.SetActive(true);
            }

            if (SevereDialogue2controller != null)
            {
                SevereDialogue2controller.ShowDialogueUI();
            }
        }
    }

}