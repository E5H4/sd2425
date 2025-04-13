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
    [SerializeField] private GameObject Dialogue1;
    [SerializeField] private GameObject Dialogue2;
    [SerializeField] private GameObject Dialogue3;
    [SerializeField] private GameObject Dialogue4;
    [SerializeField] private GameObject Dialogue5;
    [SerializeField] private GameObject Dialogue6;



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
        if (BookbagVR.minorsecond)
        {
            soda = true;
            bag.SetActive(false);
            
            {
                DeactivateAllDialogues();
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
                DeactivateAllDialogues();
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

    public void DeactivateAllDialogues()
    {
        Dialogue1?.SetActive(false);
        Dialogue2?.SetActive(false);
        Dialogue3?.SetActive(false);
        Dialogue4?.SetActive(false);
        Dialogue5?.SetActive(false);
        Dialogue6?.SetActive(false);
        MinorDialogue4?.SetActive(false);
        SodaDialogue?.SetActive(false);
    }
}

