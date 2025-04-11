using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class Barista : MonoBehaviour
{

    [Header("dialogue appears after clicking on barista")]
    [SerializeField] private GameObject barista; 
    [SerializeField] private DialogueUIController BaristaAskDialogueController; 
    [SerializeField] private GameObject baristaAsk;

    [Header("aspirin appears after clicking on barista")]
    [SerializeField] private GameObject aspirin; 

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("BaristaVR: XR Interactable component not found on " + gameObject.name);
        }

        // hidden aspirin
        if (aspirin != null)
        {
            aspirin.SetActive(false);
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
            Debug.LogWarning("BaristaAskDialogueController isn't attached properly");
        }

        // aspirin visible
        if (aspirin != null)
        {
            aspirin.SetActive(true);
            Debug.Log("Aspirin is now visible!");
        }
        else
        {
            Debug.LogWarning("Aspirin GameObject not assigned in Inspector!");
        }
    }
}