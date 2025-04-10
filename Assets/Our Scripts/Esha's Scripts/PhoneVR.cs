using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class PhoneVR : MonoBehaviour
{
    [SerializeField] private GameObject phone; 
    [SerializeField] private DialogueUIController Call911DialogueController; 
    [SerializeField] private GameObject call911Phone; 

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable; // Reference to XR interactable component

    private void Awake()
    {
        // Initialize the XR interactable component
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("PhoneVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event
            interactable.selectEntered.AddListener(OnPhoneSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            // Unsubscribe from the selectEntered event
            interactable.selectEntered.RemoveListener(OnPhoneSelected);
        }
    }

    // Called when the phone is selected (grabbed or interacted with) in VR
    private void OnPhoneSelected(SelectEnterEventArgs args)
    {
        Debug.Log("PhoneVR: Phone selected via VR.");
        
        if (phone != null)
        {
            phone.SetActive(true); // Make the phone appear or activate
        }

        // Trigger the Call 911 dialogue
        if (call911Phone != null)
        {
            call911Phone.SetActive(true);  // Display the Call 911 dialogue
        }

        // Show the dialogue UI
        if (Call911DialogueController != null)
        {
            Call911DialogueController.ShowDialogueUI();
        }
    }
}