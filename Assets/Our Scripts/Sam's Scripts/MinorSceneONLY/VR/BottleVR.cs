using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class BottleVR : MonoBehaviour
{
    [SerializeField] private GameObject tabs;
    [SerializeField] private DialogueUIController Monitor9dialoguecontroller;


    // Reference to the XR Interactable component 
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("BottleVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event
            interactable.selectEntered.AddListener(OnBagSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnBagSelected);
        }
    }

    // This is called when the Bottle is activated via VR
    private void OnBagSelected(SelectEnterEventArgs args)
    {
        Debug.Log("Bottle: Bottle selected via VR.");
        if (tabs != null)
        {
            tabs.SetActive(true); // Show the Tabs
            EnableObjects.canDragStrips = true;
            Debug.Log("Tabs appeared!");
        }

        if (Monitor9dialoguecontroller != null)
        {
            Monitor9dialoguecontroller.ShowDialogueUI();
            Debug.Log("New dialogue triggered!");
        }
        Debug.Log("BottleStrips clicked!");

        gameObject.SetActive(false); // Hide the BottleStrips
    }
}