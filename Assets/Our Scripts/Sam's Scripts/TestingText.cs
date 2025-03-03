using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TestingText : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller;
    [SerializeField] private DialogueUIController minordialoguecontroller;
    [SerializeField] private DialogueUIController severedialoguecontroller;
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    [SerializeField] private GameObject MinorDialogue;  // Reference to the MinorDialogue object
    [SerializeField] private GameObject SevereDialogue;  // Reference to the SevereDialogue object
    public static bool bookbagButton = false;

    void Start()
    {
        // Show difficulty in console
        Debug.Log("Difficulty: " + Difficulty.difficulty);

        // Load initial dialogue
        startdialoguecontroller.ShowDialogueUI();
    }

    public void bookbagTrue()
    {
        bookbagButton = true;
    }

    public void bookbagFalse()
    {
        bookbagButton = false;
    }

    public void ChoseDifficulty()
    {
        if (Difficulty.difficulty == "Minor")
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

        if (Difficulty.difficulty == "Severe")
        {
            // Disable StartDialogue
            if (StartDialogue != null)
            {
                StartDialogue.SetActive(false);
            }

            // Enable MinorDialogue
            if (SevereDialogue != null)
            {
                SevereDialogue.SetActive(true);
            }

            // Call new dialogue system
            severedialoguecontroller.ShowDialogueUI();

            Debug.Log("Major difficulty mode activated.");
        }

    }

    public void MinorDifficulty()
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

    public void MajorDifficulty()
    {
        // Disable StartDialogue
        if (StartDialogue != null)
        {
            StartDialogue.SetActive(false);
        }

        // Enable MinorDialogue
        if (SevereDialogue != null)
        {
            SevereDialogue.SetActive(true);
        }

        // Call new dialogue system
        severedialoguecontroller.ShowDialogueUI();

        Debug.Log("Major difficulty mode activated.");
    }
}