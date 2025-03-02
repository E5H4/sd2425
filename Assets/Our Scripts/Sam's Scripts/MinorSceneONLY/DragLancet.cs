using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class DragLancet : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    [SerializeField] private GameObject bigCharacter;  // Assign the character in Inspector
    [SerializeField] private GameObject bloodImage;    // Assign the blood image in Inspector
    [SerializeField] private DialogueUIController Monitor11dialoguecontroller; //Trigger dialogue

    void Start()
    {
        if (bloodImage != null)
        {
            bloodImage.SetActive(false); // Hide blood initially
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
        if (!EnableObjects.canDragLancet) // Check if dragging is allowed
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

        float distance = Vector3.Distance(transform.position, bigCharacter.transform.position);
        Debug.Log("Distance to character: " + distance); // Log distance for debugging

        if (distance < 1.87f && distance > 1.83f)  // Adjust this value if needed
        {
            Debug.Log("Lancet placed correctly!");
            EnableObjects.canDragMonitor = true;

            if (bloodImage != null)
            {
                bloodImage.SetActive(true); // Show blood image
            }

            if (Monitor11dialoguecontroller != null)
            {
                Monitor11dialoguecontroller.ShowDialogueUI(); // Trigger dialogue
                Debug.Log("New dialogue triggered!");
                StartCoroutine(HideLancetAfterDelay());
            }
        }

        IEnumerator HideLancetAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false); // Hide lancet
        }
    }
}