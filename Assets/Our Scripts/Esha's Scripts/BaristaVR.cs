using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class Barista : MonoBehaviour
{
    [SerializeField] private GameObject barista; 
    [SerializeField] private DialogueUIController BaristaAskDialogueController; 
    [SerializeField] private GameObject baristaAsk; 

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable; // Reference to XR interactable component

    private void Awake()
    {
        // Initialize the XR interactable component
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("BaristaVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event
            interactable.selectEntered.AddListener(OnBaristaSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            // Unsubscribe from the selectEntered event
            interactable.selectEntered.RemoveListener(OnBaristaSelected);
        }
    }

    // Called when touch barista (grabbed or interacted with) in VR
    private void OnBaristaSelected(SelectEnterEventArgs args)
    {
        Debug.Log("BaristaVR: Barista touched/selected via VR.");
        
        if (barista != null)
        {
            barista.SetActive(true); 
        }

        // Trigger the barista ask
        if (baristaAsk != null)
        {
            baristaAsk.SetActive(true);  // Display it
        }

        // Show the dialogue UI
        if (BaristaAskDialogueController != null)
        {
            BaristaAskDialogueController.ShowDialogueUI();
        }
         else
        {

        Debug.LogWarning("BaristaAskDialogueController isn't attched properly");
        }
        
    }
}