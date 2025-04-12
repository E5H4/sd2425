using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class MoreAEDDialoguesPlsActivate : MonoBehaviour
{
    // 3D buttons
    [SerializeField] private GameObject onButt;
    [SerializeField] private GameObject analyzeButt;
    [SerializeField] private GameObject chargeButt;
    [SerializeField] private GameObject shockButt;

    // DialogueUIControllers
    [SerializeField] private DialogueUIController onDialogueDialogueController;
    [SerializeField] private DialogueUIController analyzeDialogueDialogueController;
    [SerializeField] private DialogueUIController chargeDialogueDialogueController;
    [SerializeField] private DialogueUIController shockDialogueDialogueController;

    // Dialogue GameObjects
    [SerializeField] private GameObject onDialogue;
    [SerializeField] private GameObject analyzeDialogue;
    [SerializeField] private GameObject chargeDialogue;
    [SerializeField] private GameObject shockDialogue;

    // interactables
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable onInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable analyzeInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable chargeInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable shockInteractable;

    private bool onClicked = false;
    private bool analyzeClicked = false;
    private bool chargeClicked = false;
    private bool shockClicked = false;

    private void Awake()
    {
        
        if (onButt != null) onInteractable = onButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (analyzeButt != null) analyzeInteractable = analyzeButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (chargeButt != null) chargeInteractable = chargeButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (shockButt != null) shockInteractable = shockButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (onInteractable != null) onInteractable.selectEntered.AddListener(OnClicked);
        if (analyzeInteractable != null) analyzeInteractable.selectEntered.AddListener(AnalyzeClicked);
        if (chargeInteractable != null) chargeInteractable.selectEntered.AddListener(ChargeClicked);
        if (shockInteractable != null) shockInteractable.selectEntered.AddListener(ShockClicked);
    }

    private void OnDisable()
    {
        if (onInteractable != null) onInteractable.selectEntered.RemoveListener(OnClicked);
        if (analyzeInteractable != null) analyzeInteractable.selectEntered.RemoveListener(AnalyzeClicked);
        if (chargeInteractable != null) chargeInteractable.selectEntered.RemoveListener(ChargeClicked);
        if (shockInteractable != null) shockInteractable.selectEntered.RemoveListener(ShockClicked);
    }

    private void OnClicked(SelectEnterEventArgs args)
    {
        if (onClicked) return;
        onClicked = true;

        Debug.Log("ON button clicked");
        if (onDialogue != null) onDialogue.SetActive(true);
        if (onDialogueDialogueController != null) onDialogueDialogueController.ShowDialogueUI();
    }

    private void AnalyzeClicked(SelectEnterEventArgs args)
    {
        if (analyzeClicked) return;
        analyzeClicked = true;

        Debug.Log("ANALYZE button clicked");
        if (analyzeDialogue != null) analyzeDialogue.SetActive(true);
        if (analyzeDialogueDialogueController != null) analyzeDialogueDialogueController.ShowDialogueUI();
    }

    private void ChargeClicked(SelectEnterEventArgs args)
    {
        if (chargeClicked) return;
        chargeClicked = true;

        Debug.Log("CHARGE button clicked");
        if (chargeDialogue != null) chargeDialogue.SetActive(true);
        if (chargeDialogueDialogueController != null) chargeDialogueDialogueController.ShowDialogueUI();
    }

    private void ShockClicked(SelectEnterEventArgs args)
    {
        if (shockClicked) return;
        shockClicked = true;

        Debug.Log("SHOCK button clicked");
        if (shockDialogue != null) shockDialogue.SetActive(true);
        if (shockDialogueDialogueController != null) shockDialogueDialogueController.ShowDialogueUI();
    }
}