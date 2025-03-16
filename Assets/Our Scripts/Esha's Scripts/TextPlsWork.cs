using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TextPlsWork : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
     [SerializeField] private DialogueUIController minordialoguecontroller;
    [SerializeField] private DialogueUIController majordialoguecontroller;
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    [SerializeField] private GameObject MinorDialogue;  // Reference to the MinorDialogue object
    [SerializeField] private GameObject MajorDialogue;  // Reference to the MajorDialogue object


    // private void Start()
    // {
    //     if (startdialoguecontroller != null)
    //     {
    //         Debug.Log("Starting Dialogue...");
    //         startdialoguecontroller.ShowDialogueUI(); // This will show the dialogue panel
    //     }
    //     else
    //     {
    //         Debug.LogError("DialogueUIController is not assigned.");
    //     }
    // }

        void Start() {
        // Show difficulty in console if there is one set lol
            if (string.IsNullOrEmpty(SetDifficulty.difficulty)) {
                Debug.LogError("SETDIFFICULTY.DIFFICULTY IS NULL OR EMPTY!");
            } else {
                Debug.Log("Difficulty: " + SetDifficulty.difficulty);
            }
        // Load initial dialogue
        startdialoguecontroller.ShowDialogueUI();
    }

        public void ChoseDifficulty() {
        if (SetDifficulty.difficulty == "Minor")
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

            Debug.Log("Minor difficulty mode activated!");
        }

        if (SetDifficulty.difficulty == "Major")
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

            Debug.Log("Major difficulty mode activated!");
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

        Debug.Log("Minor difficulty mode activated!");
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

        Debug.Log("Major difficulty mode activated!");
    }

}