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
        if (Difficulty.difficulty == "Minor")
        {
            if (KitDialogue != null)
            {
                DeactivateAllDialogues();
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
                DeactivateAllDialogues();
                SevereDialogue2.SetActive(true);
            }

            if (SevereDialogue2controller != null)
            {
                SevereDialogue2controller.ShowDialogueUI();
            }
        }
    }

    public void DeactivateAllDialogues()
    {
        Dialogue1?.SetActive(false);
        Dialogue2?.SetActive(false);
        Dialogue3?.SetActive(false);
        Dialogue4?.SetActive(false);
        Dialogue5?.SetActive(false);
        Dialogue6?.SetActive(false);
        KitDialogue?.SetActive(false);
        SevereDialogue2?.SetActive(false);
    }

}