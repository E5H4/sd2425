using UnityEngine;
using DialogueSystemWithText;

public class SyringeDraggable : MonoBehaviour
{
    // Dialogue for when the syringe is inserted into the vile (Step 2)
    [SerializeField] private DialogueUIController severeDialogue4;

    // Reference to the vile’s transform (assign via Inspector)
    [SerializeField] private Transform vileTransform;

    // Draggable mechanics
    private bool isDragging = false;
    private Vector3 offset;

    // Distance threshold for considering the syringe “inserted”
    private float insertionThreshold = 1.0f;

    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePos = Input.mousePosition;
        float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = objectZ;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePos = Input.mousePosition;
        float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = objectZ;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;
        newPos.z = transform.position.z; // Ensure z remains constant.
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (vileTransform != null)
        {
            float distance = Vector3.Distance(transform.position, vileTransform.position);
            Debug.Log("SyringeDraggable: Distance to vile: " + distance);
            if (distance < insertionThreshold)
            {
                Debug.Log("SyringeDraggable: Syringe inserted into vile. Triggering dialogue.");
                if (severeDialogue4 != null)
                {
                    severeDialogue4.ShowDialogueUI();
                }
                // Optionally disable further dragging.
                this.enabled = false;
            }
        }
    }
}