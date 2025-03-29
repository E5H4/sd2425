using UnityEngine;
using DialogueSystemWithText;

public class SyringeDraggable : MonoBehaviour
{
    [SerializeField] private DialogueUIController severeDialogue5;
    [SerializeField] private Transform vileTransform;
    [SerializeField] private Transform vileTopTransform;
    [SerializeField] private GameObject bottleSyringeObject;

    // Draggable mechanics
    private bool isDragging = false;
    private Vector3 offset;

    //considering the syringe “inserted” (general proximity to vile)
    private float insertionThreshold = 0.55f;

    //syringe's needle to be at the top of the vile.
    private float topThreshold = 0.1f;

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

            // Check both that the syringe is within the general insertion range
            // and that the needle is at the top of the vile.
            if (distance < insertionThreshold && IsNeedleAtTop())
            {
                Debug.Log("SyringeDraggable: Needle is in the top of the vile. Triggering dialogue and activating combined object.");
                if (severeDialogue5 != null)
                {
                    severeDialogue5.ShowDialogueUI();
                }
                // Activate the combined bottleSyringe object.
                if (bottleSyringeObject != null)
                {
                    bottleSyringeObject.SetActive(true);
                }
           
                gameObject.SetActive(false);
     
                if (vileTransform != null)
                {
                    vileTransform.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("SyringeDraggable: Needle is not at the top of the vile.");
            }
        }
    }

    //checks if the syringe's position is in the top of the vile.
    private bool IsNeedleAtTop()
    {
        if (vileTopTransform == null)
        {
            Debug.LogWarning("SyringeDraggable: Vile top transform not assigned.");
            return false;
        }
        float topDistance = Vector3.Distance(transform.position, vileTopTransform.position);
        Debug.Log("SyringeDraggable: Distance to vile top: " + topDistance);
        return topDistance < topThreshold;
    }
}