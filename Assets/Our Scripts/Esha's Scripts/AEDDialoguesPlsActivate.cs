using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class AEDDialoguesPlsActivate : MonoBehaviour
{
    [Header("3D Buttons")]
    [SerializeField] private GameObject onButt;
    [SerializeField] private GameObject analyzeButt;
    [SerializeField] private GameObject chargeButt;
    [SerializeField] private GameObject shockButt;
    [SerializeField] private GameObject bag1;

    [Header("Dialogue Controllers")]
    [SerializeField] private DialogueUIController onDialogueDialogueController;
    [SerializeField] private DialogueUIController analyzeDialogueDialogueController;
    [SerializeField] private DialogueUIController chargeDialogueDialogueController;
    [SerializeField] private DialogueUIController shockDialogueDialogueController;
    [SerializeField] private DialogueUIController bag1DialogueDialogueController;

    [Header("Dialogue GameObjects")]
    [SerializeField] private GameObject onDialogue;
    [SerializeField] private GameObject analyzeDialogue;
    [SerializeField] private GameObject chargeDialogue;
    [SerializeField] private GameObject shockDialogue;
    [SerializeField] private GameObject bag1Dialogue;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable onInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable analyzeInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable chargeInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable shockInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable bag1Interactable;

    private bool onClicked = false;
    private bool analyzeClicked = false;
    private bool chargeClicked = false;
    private bool shockClicked = false;
    private bool bag1Clicked = false;

    private void Awake()
    {
        if (onButt != null) onInteractable = onButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (analyzeButt != null) analyzeInteractable = analyzeButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (chargeButt != null) chargeInteractable = chargeButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (shockButt != null) shockInteractable = shockButt.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (bag1 != null) bag1Interactable = bag1.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (onInteractable != null) onInteractable.selectEntered.AddListener(OnClicked);
        if (analyzeInteractable != null) analyzeInteractable.selectEntered.AddListener(AnalyzeClicked);
        if (chargeInteractable != null) chargeInteractable.selectEntered.AddListener(ChargeClicked);
        if (shockInteractable != null) shockInteractable.selectEntered.AddListener(ShockClicked);
        if (bag1Interactable != null) bag1Interactable.selectEntered.AddListener(Bag1Clicked);
    }

    private void OnDisable()
    {
        if (onInteractable != null) onInteractable.selectEntered.RemoveListener(OnClicked);
        if (analyzeInteractable != null) analyzeInteractable.selectEntered.RemoveListener(AnalyzeClicked);
        if (chargeInteractable != null) chargeInteractable.selectEntered.RemoveListener(ChargeClicked);
        if (shockInteractable != null) shockInteractable.selectEntered.RemoveListener(ShockClicked);
        if (bag1Interactable != null) bag1Interactable.selectEntered.RemoveListener(Bag1Clicked);
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
        if (!onClicked || analyzeClicked) return;
        analyzeClicked = true;

        Debug.Log("ANALYZE button clicked");
        if (analyzeDialogue != null) analyzeDialogue.SetActive(true);
        if (analyzeDialogueDialogueController != null) analyzeDialogueDialogueController.ShowDialogueUI();
    }

    private void ChargeClicked(SelectEnterEventArgs args)
    {
        if (!analyzeClicked || chargeClicked) return;
        chargeClicked = true;

        Debug.Log("CHARGE button clicked");
        if (chargeDialogue != null) chargeDialogue.SetActive(true);
        if (chargeDialogueDialogueController != null) chargeDialogueDialogueController.ShowDialogueUI();
    }

    private void ShockClicked(SelectEnterEventArgs args)
    {
        if (!chargeClicked || shockClicked) return;
        shockClicked = true;

        Debug.Log("SHOCK button clicked");
        if (shockDialogue != null) shockDialogue.SetActive(true);
        if (shockDialogueDialogueController != null) shockDialogueDialogueController.ShowDialogueUI();
    }

    private void Bag1Clicked(SelectEnterEventArgs args)
    {
        if (bag1Clicked) return;
        bag1Clicked = true;

        Debug.Log("BAG1 button clicked");
        if (bag1Dialogue != null) bag1Dialogue.SetActive(true);
        if (bag1DialogueDialogueController != null) bag1DialogueDialogueController.ShowDialogueUI();
    }
}
