using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TextPlsWork : MonoBehaviour
{
    [SerializeField] private DialogueUIController _dialogueUIController; // this is my start dialogue test (DIALOGUEUI) , premade and prenamed
     [SerializeField] private DialogueUIController minordialoguecontroller;
    [SerializeField] private DialogueUIController majordialoguecontroller;
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    [SerializeField] private GameObject MinorDialogue;  // Reference to the MinorDialogue object
    [SerializeField] private GameObject MajorDialogue;  // Reference to the SevereDialogue object


    private void Start()
    {
        if (_dialogueUIController != null)
        {
            Debug.Log("Starting Dialogue...");
            _dialogueUIController.ShowDialogueUI(); // This will show the dialogue panel
        }
        else
        {
            Debug.LogError("DialogueUIController is not assigned.");
        }
    }

    public void MinorDifficulty() // minor/female version
    {
        // Disable StartDialogue
        if (StartDialogue != null)
        {
            StartDialogue.SetActive(false);
        }

        // Enable MinorDialogue
        if (MinorDialogue != null)
        {
            MinorDialogue.SetActive(true);
        }

        // Call new dialogue system
        minordialoguecontroller.ShowDialogueUI();

        Debug.Log("Minor difficulty mode activated.");
    }

    public void MajorDifficulty() //severe/male version
    {
        // Disable StartDialogue
        if (StartDialogue != null)
        {
            StartDialogue.SetActive(false);
        }

        // Enable MajorDialogue
        if (MajorDialogue != null)
        {
            MajorDialogue.SetActive(true);
        }

        // Call new dialogue system
        majordialoguecontroller.ShowDialogueUI();

        Debug.Log("Major difficulty mode activated.");
    }

}