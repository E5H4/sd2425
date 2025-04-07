using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class Pop : MonoBehaviour
{
    [SerializeField] private DialogueUIController minordialogue4controller;
    [SerializeField] private GameObject MinorDialogue4;
    [SerializeField] private DialogueUIController SodaDialoguecontroller;
    [SerializeField] private GameObject SodaDialogue;
    [SerializeField] private GameObject bag;
    public static bool soda = false;



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
        if (Bookbag.minorsecond)
        {
            soda = true;
            bag.SetActive(false);
            
            {
                MinorDialogue4.SetActive(true);
                Debug.Log("Minor dialogue 4 activated.");
            }

            if (minordialogue4controller != null)
            {
                minordialogue4controller.ShowDialogueUI();
            }
        }
        else
        {
            if (SodaDialogue != null)
            {
                SodaDialogue.SetActive(true);
            }

            if (SodaDialoguecontroller != null)
            {
                SodaDialoguecontroller.ShowDialogueUI();
            }
        }


    }


    public void debugmessage()
    {
        Debug.Log("I AM CALLED.");
    }
}

