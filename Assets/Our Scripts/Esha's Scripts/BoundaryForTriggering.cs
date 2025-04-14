using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class BoundaryForTriggering : MonoBehaviour
{
    [Header("Triggered when the player enters this boundary")]
    [SerializeField] private DialogueUIController dialogueController;
    [SerializeField] private GameObject dialogue;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Make sure it's only triggered once AND only by the player
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        if (dialogue != null)
        {
            dialogue.SetActive(true);
            Debug.Log("AED or MedKit dialogue activated.");
        }

        if (dialogueController != null)
        {
            dialogueController.ShowDialogueUI();
            Debug.Log("Dialogue controller triggered.");
        }
    }
}