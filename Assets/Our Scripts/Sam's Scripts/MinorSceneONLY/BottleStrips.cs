using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class BottleStrips : MonoBehaviour
{
    [SerializeField] private GameObject tabs; // Assign Tabs object in Inspector
    [SerializeField] private DialogueUIController Monitor9dialoguecontroller;

    void Start()
    {
        if (tabs != null)
        {
            tabs.SetActive(false); // Hide Tabs initially
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject) // If clicked object is this one (BottleStrips)
                {
                    if (tabs != null)
                    {
                        tabs.SetActive(true); // Show the Tabs
                        EnableObjects.canDragStrips = true;
                        Debug.Log("Tabs appeared!");
                    }

                    if (Monitor9dialoguecontroller != null)
                    {
                        Monitor9dialoguecontroller.ShowDialogueUI();
                        Debug.Log("New dialogue triggered!");
                    }
                    Debug.Log("BottleStrips clicked!");

                    gameObject.SetActive(false); // Hide the BottleStrips
                }
            }
        }
    }

}
