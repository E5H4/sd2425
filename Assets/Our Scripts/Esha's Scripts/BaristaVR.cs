using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class BaristaVR : MonoBehaviour
{
    [SerializeField] private GameObject barista; 
    [SerializeField] private DialogueUIController BaristaAskDialogueController; 
    [SerializeField] private GameObject baristaAsk; 

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool hasInteracted = false;

    private void Awake()
    {
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
            interactable.selectEntered.AddListener(OnBaristaSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnBaristaSelected);
        }
    }

    private void OnBaristaSelected(SelectEnterEventArgs args)
    {
        if (hasInteracted) return; // Prevent repeat interaction

        hasInteracted = true; // Set flag to true after first interaction

        Debug.Log("BaristaVR: Barista touched/selected via VR.");

        if (barista != null)
        {
            barista.SetActive(true); 
        }

        if (baristaAsk != null)
        {
            baristaAsk.SetActive(true);
        }

        if (BaristaAskDialogueController != null)
        {
            BaristaAskDialogueController.ShowDialogueUI();
        }
        else
        {
            Debug.LogWarning("BaristaAskDialogueController isn't attached properly.");
        }
    }
}