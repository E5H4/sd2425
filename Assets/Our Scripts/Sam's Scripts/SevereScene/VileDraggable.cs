using System.Collections;
using UnityEngine;
using DialogueSystemWithText;

[RequireComponent(typeof(Collider))]
public class VileDraggable : MonoBehaviour
{
    [SerializeField] private DialogueUIController severeDialogue3;
    [SerializeField] private DialogueUIController severeDialogue4;
    [SerializeField] private GameObject severedialogue4;
    // Drag and double-click variables
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 mouseDownPos;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    private float dragThreshold = 0.1f; // World units
    private bool actionCompleted = false;

    private void Update()
    {
        // Continuously check if the vile's z rotation is near 180°.
        if (!actionCompleted)
        {
            float currentZ = NormalizeAngle(transform.eulerAngles.z);
            if (Mathf.Abs(currentZ - 180f) < 5f)
            {
                Debug.Log("VileDraggable: Detected rotation near 180° in Update. Triggering dialogue.");
                severedialogue4.SetActive(true);
                if (severeDialogue4 != null)
                {
                    severeDialogue3.HideDialogueUI();
                    severeDialogue4.ShowDialogueUI();
                }
                actionCompleted = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (actionCompleted) return;

        isDragging = true;
        // Record the initial mouse position 
        Vector3 mousePos = Input.mousePosition;
        float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = objectZ;
        mouseDownPos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - mouseDownPos;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePos = Input.mousePosition;
        float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = objectZ;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;
        newPos.z = transform.position.z; // Keep the z value unchanged.
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        // Determine if the mouse moved far enough to be considered a drag.
        Vector3 mouseUpPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(transform.position).z));
        float dragDistance = Vector3.Distance(mouseDownPos, mouseUpPos);

        // If the movement was minimal, treat it as a click.
        if (dragDistance < dragThreshold)
        {
            if (Time.time - lastClickTime <= doubleClickThreshold)
            {
                // Double-click detected: rotate by 45°.
                transform.Rotate(0, 0, 45);
                Debug.Log("VileDraggable: Vile rotated 45° on double click.");

                // Check if the vile is near 180° (upside down).
                float zRotation = NormalizeAngle(transform.eulerAngles.z);
                if (Mathf.Abs(zRotation - 180f) < 5f)
                {
                    Debug.Log("VileDraggable: Vile is approximately upside down. Triggering dialogue.");
                    if (severeDialogue4 != null)
                    {
                        severeDialogue3.HideDialogueUI();
                        severeDialogue4.ShowDialogueUI();
                    }
                    actionCompleted = true;
                }
                lastClickTime = 0f;
            }
            else
            {
                lastClickTime = Time.time;
            }
        }
    }

    //normalize an angle between 0 and 360.
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle < 0)
            angle += 360f;
        return angle;
    }
}