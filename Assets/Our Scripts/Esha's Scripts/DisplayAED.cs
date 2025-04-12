using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class DisplayAED : MonoBehaviour
{
    [Header("dialogues are under 1 ui")]
    [SerializeField] private DialogueUIController dialogueController;

    [Header("assign dialogues for each")]
    [SerializeField] private GameObject onDialogue;
    [SerializeField] private GameObject analyzeDialogue;
    [SerializeField] private GameObject chargeDialogue;
    [SerializeField] private GameObject shockDialogue;

    // methods trigger dialogues for each button
    // these are called under each button in "select entered"
    
    public void triggerOnDialogue()
    {
        if (onDialogue != null)
        {
            onDialogue.SetActive(true);
            dialogueController.ShowDialogueUI();
            Debug.Log("On Button Dialogue triggered.");
        }
    }

    public void triggerAnalyzeDialogue()
    {
        if (analyzeDialogue != null)
        {
            analyzeDialogue.SetActive(true);
            dialogueController.ShowDialogueUI();
            Debug.Log("Analyze Button Dialogue triggered.");
        }
    }

    public void triggerChargeDialogue()
    {
        if (chargeDialogue != null)
        {
            chargeDialogue.SetActive(true);
            dialogueController.ShowDialogueUI();
            Debug.Log("Charge Button Dialogue triggered.");
        }
    }

    public void triggerShockDialogue()
    {
        if (shockDialogue != null)
        {
            shockDialogue.SetActive(true);
            dialogueController.ShowDialogueUI();
            Debug.Log("Shock Button Dialogue triggered.");
        }
    }
}