using UnityEngine;
using DialogueSystemWithText;

public class ClickableCharacter : MonoBehaviour
{
    [SerializeField] private GameObject MinorDialogue4;  // Assign in Inspector
    [SerializeField] private GameObject MinorDialogue5;  // Assign in Inspector
    [SerializeField] private DialogueUIController minordialogue5controller;

    void OnMouseDown()
    {
        Debug.Log("Character clicked!");

        // Check if soda is true before proceeding
        if (Bookbag.soda)
        {
            Debug.Log("Soda is true! Switching dialogues.");

            // Disable MinorDialogue4 if it's active
            if (MinorDialogue4 != null && MinorDialogue4.activeSelf)
            {
                MinorDialogue4.SetActive(false);
                Debug.Log("MinorDialogue4 turned off.");
            }

            // Enable MinorDialogue5
            if (MinorDialogue5 != null)
            {
                MinorDialogue5.SetActive(true);
                Debug.Log("MinorDialogue5 activated!");
                minordialogue5controller.ShowDialogueUI();
            }
        }
        else
        {
            Debug.Log("Soda is not true yet, cannot proceed.");
        }
    }
}