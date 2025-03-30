using UnityEngine;
using DialogueSystemWithText;

public class SyringeShotDraggable : MonoBehaviour
{
    // Flag indicating whether the syringe is being dragged.
    private bool isDragging = false;
    // Offset between the object's position and the mouse's world position at the start of the drag.
    private Vector3 offset;
    // The fixed Z offset from the camera.
    private float fixedZOffset;

    // Dialogue to turn off (e.g., if Dialogue 10 is already active).
    [SerializeField] private DialogueUIController severeDialogue10;
    // Dialogue to turn on when the needle is inserted (Dialogue 11).
    [SerializeField] private DialogueUIController severeDialogue11;

    private void OnMouseDown()
    {
        isDragging = true;
        // Get current mouse position in screen space.
        Vector3 mousePos = Input.mousePosition;
        // Set its z to the object's current screen z position.
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        // Convert to world space.
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // Calculate the offset.
        offset = transform.position - worldPos;
        // Compute the Z offset from the camera.
        fixedZOffset = transform.position.z - Camera.main.transform.position.z;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
            return;

        // Get the updated mouse position in screen space.
        Vector3 mousePos = Input.mousePosition;
        // Preserve the object's screen z value.
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // Update position using the initial offset.
        Vector3 newPos = worldPos + offset;
        // Set z relative to the camera's current z plus the fixed offset.
        newPos.z = Camera.main.transform.position.z + fixedZOffset;
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    // When the syringe (or its needle) collides with a target collider.
    private void OnTriggerEnter(Collider other)
    {
        // Make sure the target collider is tagged as "NeedleTarget".
        if (other.CompareTag("NeedleTarget"))
        {
            Debug.Log("SyringeShotDraggable: Needle entered target area: " + other.name);
            // Turn off Dialogue 10.
            if (severeDialogue10 != null)
            {
                severeDialogue10.gameObject.SetActive(false);
            }
            // Activate and show Dialogue 11.
            if (severeDialogue11 != null)
            {
                severeDialogue11.gameObject.SetActive(true);
                severeDialogue11.ShowDialogueUI();
            }
        }
    }
}