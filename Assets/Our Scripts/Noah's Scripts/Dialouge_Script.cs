using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class BulletWoundDialougue : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private DialogueUIController midscenariodialoguecontroller;
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    [SerializeField] private GameObject midDialogue; // Reference to the Mid-section dialogue
    
          void Start() {   
        // Load initial dialogue
        startdialoguecontroller.ShowDialogueUI();
    }
        void midscenarioDialogue(){
            //Disable begining dialouge 
            if (StartDialogue != null){
                StartDialogue.SetActive(false);
            }
            //Enables Midsection dialogue
            if(midDialogue != null){
                midDialogue.SetActive(true);
            }
            midscenariodialoguecontroller.ShowDialogueUI();
            //debug.log ("I AM WORKING!!");
        }

}