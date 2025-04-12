using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape_interaction : MonoBehaviour
{
    
   public GameObject pressureGameBegin; //this is a name for the pressure game to st
   public string medicalTapeObjectTag = "Medical Tape"; //this tag is so no other object can interact an trigger the mini game

   private void pressureGameTrigger(Collider other)
   {
    if(other.CompareTag(medicalTapeObjectTag))
    {
        //logs to the console that the pressure game has been activated
        Debug.Log("PRESSURE GAME ACTIVATED!!!!!");
        if(pressureGameBegin != null) 
        {
            pressureGameBegin.SetActive(true); //this trigger the game to then begin
        }

        //This will destroy the Medical tape object causing it to not re-appear
        Destroy(other.gameObject);
    }
   }
    
}
