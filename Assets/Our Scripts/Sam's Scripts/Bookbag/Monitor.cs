using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class Meter : MonoBehaviour
{
    [SerializeField] private DialogueUIController minordialogue2controller;
    [SerializeField] private GameObject MinorDialogue2;
    [SerializeField] private DialogueUIController MeterDialoguecontroller;
    [SerializeField] private GameObject MeterDialogue;

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
            if (Bookbag.minorsecond == false)
            {
                if (bag.activeInHierarchy)
                {
                    bag.SetActive(false);
                }

                if (MinorDialogue2 != null)
                {
                    MinorDialogue2.SetActive(true);
                    Debug.Log("Minor dialogue 2 activated.");
                    minordialogue2controller.ShowDialogueUI();
                }

            }
            else
            {
                if (MeterDialogue != null)
                {
                    MeterDialogue.SetActive(true);
                    Debug.Log("Meter dialogue activated.");
                }

                if (MeterDialoguecontroller != null)
                {
                    MeterDialoguecontroller.ShowDialogueUI();
                }

            }
        }
        else
        {
            if (MeterDialogue != null)
            {
                MeterDialogue.SetActive(true);
                Debug.Log("Meter dialogue activated.");
            }

            if (MeterDialoguecontroller != null)
            {
                MeterDialoguecontroller.ShowDialogueUI();
            }

        }
    }
}

