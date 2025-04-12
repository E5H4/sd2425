using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;


public class Noahs_Backpack : MonoBehaviour
{
   
    [SerializeField] private DialogueUIController startdialoguecontroller2; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private DialogueUIController midscenariodialoguecontroller2;
    [SerializeField] private DialogueUIController endscenariodialoguecontroller2;
    [SerializeField] private DialogueUIController gauzeDialogueController;
    [SerializeField] private DialogueUIController phoneDialogueController;
    [SerializeField] private DialogueUIController cannedSodaDrinkController;
    [SerializeField] private GameObject StartDialogue2;  // Reference to the StartDialogue object
    [SerializeField] private GameObject midDialogue2;
    [SerializeField] private GameObject endDialogue2;
    [SerializeField] private GameObject GauzeDialogue;
    [SerializeField] private GameObject PhoneDialogue;
    [SerializeField] private GameObject cannedSodaDrinkDialogue;
    [SerializeField] private GameObject backpack;


    /*
    // Start is called before the first frame update
    void Start()
    {
       
    }
    */
    // Update is called once per frame
  void Update()
    {
        if (Input.GetMouseButtonDown(0)) //checks if players clicks anything
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit) ) //
            {
                if (hit.collider.gameObject == gameObject) //check if player actually clicked it
                {
                    Debug.Log("Backpack has been clicked chat!!");
                    if (backpack != null)
                    {
                        backpack.SetActive(true);
                    }
                }
            }
        }
    }
   
    public void dialoguesTurnsOff() {
        GauzeDialogue.SetActive(false);
        PhoneDialogue.SetActive(false);
        cannedSodaDrinkDialogue.SetActive(false);
    }

     public void cannedDrinkDialogue(){
        dialoguesTurnsOff();
        if( cannedSodaDrinkDialogue != null){
            cannedSodaDrinkDialogue.SetActive(true);
        }
        if ( cannedSodaDrinkController!= null){
            cannedSodaDrinkController.gameObject.SetActive(true);
        }
        cannedSodaDrinkController.ShowDialogueUI();
    }
    
    public void gauzeDialogue (){
        dialoguesTurnsOff();
        if( GauzeDialogue != null){
            GauzeDialogue.SetActive(true);
        }
        if ( gauzeDialogueController!= null){
            gauzeDialogueController.gameObject.SetActive(true);
        }
        gauzeDialogueController.ShowDialogueUI();
    }
    public void phoneDialogue() {
        dialoguesTurnsOff();
        if( PhoneDialogue != null){
             PhoneDialogue.SetActive(true);
        }
        if ( phoneDialogueController!= null){
            phoneDialogueController.gameObject.SetActive(true);
        }
        phoneDialogueController.ShowDialogueUI();
    }
        //DATE AS OF 4/6/25
   
}
