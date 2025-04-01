using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class BulletWoundDialougue : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    public static bool backpackButton = false;


    // private void Start()
    // {
    //     if (startdialoguecontroller != null)
    //     {
    //         Debug.Log("Starting Dialogue...");
    //         startdialoguecontroller.ShowDialogueUI(); 
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
    // Code for the back pack
     public void backpackTrue()
    {
        backpackButton = true;
    }

    public void backpackFalse()
    {
        backpackButton = false;
    }


}