using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class BulletWoundDialougue : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object

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
            
        // Load initial dialogue
        startdialoguecontroller.ShowDialogueUI();
    }

}