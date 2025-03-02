using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class DragStrip : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    [SerializeField] private GameObject monitorTop;  // Assign the top of the glucose monitor in Inspector
    [SerializeField] private GameObject monitorTab;  // Assign the new MonitorTab UI in Inspector
    [SerializeField] private DialogueUIController Monitor10dialoguecontroller;

    void Start()
    {
        if (monitorTab != null)
        {
            monitorTab.SetActive(false); // Hide MonitorTab initially
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Keep the correct depth
            Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;
            newPos.z = transform.position.z; // Lock the Z position
            transform.position = newPos;
        }
    }

    void OnMouseDown()
    {
        if (!EnableObjects.canDragStrips) // Check if dragging is allowed
        {
            Debug.Log("Dragging not allowed yet!");
            return; // Stop execution if dragging is not allowed
        }

        isDragging = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        offset = transform.position - mouseWorldPos;
    }

    IEnumerator HideStripAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false); // Hide the strip (Tabs)
        if (monitorTab != null)
        {
            monitorTab.SetActive(true); // Show MonitorTab
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        float distance = Vector3.Distance(transform.position, monitorTop.transform.position);
        Debug.Log("Distance to monitor top: " + distance); // Log actual distance

        if (distance < 0.2f)  // This value might need adjusting
        {
            Debug.Log("Strip placed correctly!");
            EnableObjects.canDragLancet = true;
            StartCoroutine(HideStripAfterDelay());
            if (Monitor10dialoguecontroller != null)
            {
                Monitor10dialoguecontroller.ShowDialogueUI();
                Debug.Log("New dialogue triggered!");
            }
        }
    }
}