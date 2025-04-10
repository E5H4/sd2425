using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class PhoneWORKNOW : MonoBehaviour
{
    [SerializeField] private GameObject phone;  // Reference to the phone object
    [SerializeField] private DialogueUIController Call911DialogueController;  // Dialogue controller for the 911 call
    [SerializeField] private GameObject call911Phone;  // The GameObject representing the Call 911 dialogue

    void Update()
    {
        // Check for mouse click on the phone object
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the phone was clicked
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Phone clicked");
                    if (phone != null)
                    {
                        phone.SetActive(true);  // Show the phone when clicked
                    }

                    // Activate the 911 call dialogue
                    if (call911Phone != null)
                    {
                        call911Phone.SetActive(true);  // Display the Call 911 dialogue
                    }

                    // Show the dialogue UI
                    if (Call911DialogueController != null)
                    {
                        Call911DialogueController.ShowDialogueUI();
                    }
                }
            }
        }
    }
}