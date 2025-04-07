using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class ItemsDialogueScript : MonoBehaviour
{
    [SerializeField] private DialogueUIController dialoguecontroller;
    [SerializeField] private GameObject dialogue;


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
        Debug.Log("Obj: Obj selected via VR.");

        dialogue.SetActive(true);
        dialoguecontroller.ShowDialogueUI();


    }


    public void debugmessage()
    {
        Debug.Log("I AM CALLED.");
    }
}

