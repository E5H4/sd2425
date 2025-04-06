using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class Noahs_Backpack : MonoBehaviour
{
    
    [SerializeField] private DialogueUIController startdialoguecontroller2; // this is my StartDialogue (DIALOGUEUI) , premade and prenamed
    [SerializeField] private DialogueUIController midscenariodialoguecontroller2;
    [SerializeField] private DialogueUIController endscenariodialoguecontroller2;
    [SerializeField] private DialogueUIController walletDialogueController;
    [SerializeField] private DialogueUIController towelDialogueController;
    [SerializeField] private DialogueUIController gauzeDialogueController;
    [SerializeField] private DialogueUIController waterbottleDialogueController;
    [SerializeField] private DialogueUIController phoneDialogueController;
    [SerializeField] private GameObject StartDialogue2;  // Reference to the StartDialogue object
    [SerializeField] private GameObject midDialogue2;
    [SerializeField] private GameObject endDialogue2;
    [SerializeField] private GameObject WalletDialogue;
    [SerializeField] private GameObject TowelDialogue;
    [SerializeField] private GameObject GauzeDialogue;
    [SerializeField] private GameObject WaterBottleDialogue;
    [SerializeField] private GameObject PhoneDialogue;
    [SerializeField] private GameObject backpack;

/*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    public void Updated()
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
    
    private void dialoguesTurnsOff() {
        TowelDialogue.SetActive(false);
        WalletDialogue.SetActive(false);
        WaterBottleDialogue.SetActive(false);
        GauzeDialogue.SetActive(false);
        PhoneDialogue.SetActive(false);
    }

    public void towelDialogue() {
        dialoguesTurnsOff();
        if(TowelDialogue != null){
            TowelDialogue.SetActive(true);
        }
        if(towelDialogueController != null){
            towelDialogueController.gameObject.SetActive(true);
        }
        towelDialogueController.ShowDialogueUI();
    }
    public void walletDialogue() {
        dialoguesTurnsOff();
        if( WalletDialogue != null){
            WalletDialogue.SetActive(true);
        }
        if ( walletDialogueController != null){
            walletDialogueController.gameObject.SetActive(true);
        }
        walletDialogueController.ShowDialogueUI();
    }
    public void waterbottleDialogue() {
        dialoguesTurnsOff();
        if( WaterBottleDialogue != null){
            WaterBottleDialogue.SetActive(true);
        }
        if ( waterbottleDialogueController != null){
            waterbottleDialogueController.gameObject.SetActive(true);
        }
        walletDialogueController.ShowDialogueUI();

    }
    public void gauzeDialogue (){
        dialoguesTurnsOff();
        if( GauzeDialogue != null){
            GauzeDialogue.SetActive(true);
        }
        if ( gauzeDialogueController!= null){
            gauzeDialogueController.gameObject.SetActive(true);
        }
        walletDialogueController.ShowDialogueUI();
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
        //DATE AS OF 4/5/25
    
}
