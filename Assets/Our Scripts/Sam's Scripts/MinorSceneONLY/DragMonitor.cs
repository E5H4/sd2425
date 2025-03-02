using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class DragMonitor : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    [SerializeField] private GameObject bigCharacter;  // Assign the character in Inspector
    [SerializeField] private GameObject monitorTab;    // Assign the monitor tab in Inspector
    [SerializeField] private GameObject monitorTop;    // Assign the monitor top in Inspector
    [SerializeField] private GameObject text;    // Assign the blood image in Inspector
    [SerializeField] private DialogueUIController MinorDialogue3; // Trigger next dialogue

    void Start()
    {
        if (text != null)
        {
            text.SetActive(false); // Hide blood initially
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain correct depth
            Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;
            newPos.z = transform.position.z; // Keep Z position fixed
            transform.position = newPos;
        }
    }

    void OnMouseDown()
    {
        if (!EnableObjects.canDragMonitor) // Check if dragging is allowed
        {
            Debug.Log("Dragging not allowed yet!");
            return; // Stop execution if dragging is not allowed
        }
        isDragging = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if either the monitorTab or monitorTop is near the character
        if (IsNearCharacter(monitorTab) || IsNearCharacter(monitorTop))
        {
            Debug.Log("Monitor placed correctly!");

            if (text != null)
            {
                text.SetActive(true); // Show blood image
            }

            if (MinorDialogue3 != null)
            {
                MinorDialogue3.ShowDialogueUI(); // Trigger next dialogue
                Debug.Log("MinorDialogue3 triggered!");
            }
        }
    }

    bool IsNearCharacter(GameObject obj)
    {
        float distance = Vector3.Distance(obj.transform.position, bigCharacter.transform.position);
        Debug.Log(obj.name + " Distance to character: " + distance);
        return (distance < 2.0f && distance > 1.85f); // Ensures it's within the correct range
    }
}