using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class AEDPadsChecker : MonoBehaviour
{
    public DialogueUIController dialogueController;
    public GameObject dialogueUI;

    private bool leftPadTouched = false;
    private bool rightPadTouched = false;

    public void RegisterPadHit(string padName)
    {
        if (padName == "LeftPadToDrag")
            leftPadTouched = true;
        else if (padName == "RightPadToDrag")
            rightPadTouched = true;

        Debug.Log($"AEDPadsChecker: {padName} registered. Left: {leftPadTouched}, Right: {rightPadTouched}"); //check w booleans

        if (leftPadTouched && rightPadTouched)
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(true);

        if (dialogueController != null)
        {
            dialogueController.ShowDialogueUI();
            Debug.Log("dialogue triggered, after both pads touched");
        }
    }
}