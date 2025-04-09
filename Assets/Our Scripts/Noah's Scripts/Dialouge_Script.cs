using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;


public class BulletWoundDialougue : MonoBehaviour
{
    [SerializeField] private DialogueUIController startdialoguecontroller; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private DialogueUIController midscenariodialoguecontroller;
    [SerializeField] private DialogueUIController endscenariodialoguecontroller;
    [SerializeField] private GameObject StartDialogue;  // Reference to the StartDialogue object
    [SerializeField] private GameObject midDialogue;
     [SerializeField] private GameObject endDialogue;
    public static bool backpackButton = false;




        public void Start() {
        Debug.Log("Im being callled in start");
        // Show difficulty in console if there is one set lol
           
        // Load initial dialogue
        startdialoguecontroller.ShowDialogueUI();
    }




        public void midscenarioDialogue(){
            Debug.Log("Im being callled in mid");
            //disables the starting dialogue
            if(StartDialogue != null){
                StartDialogue.SetActive(false);
            }
            //Enable Mid-section Dialogue
            if(midDialogue != null){
                midDialogue.SetActive(true);
            }
            Debug.Log("Im working in mid!!");
            midscenariodialoguecontroller.ShowDialogueUI();
        }
       
         public void endscenarioDialogue(){
            Debug.Log("Im being callled in mid");
            //disables the starting dialogue
            if(StartDialogue != null){
                StartDialogue.SetActive(false);
            }
            //disable Mid-section Dialogue
            if(midDialogue != null){
                midDialogue.SetActive(false);
            }
            //enables end dialog
             if(endDialogue != null){
                endDialogue.SetActive(true);
            }


            Debug.Log("Im working in End!!");
            endscenariodialoguecontroller.ShowDialogueUI();
        }
  public void midscenarioDialogue2(){
           
            midscenariodialoguecontroller.ShowDialogueUI();
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
