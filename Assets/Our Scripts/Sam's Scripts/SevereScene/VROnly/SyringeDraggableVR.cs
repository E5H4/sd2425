using System.Collections;
using UnityEngine;

using DialogueSystemWithText;

public class SyringeShotDraggableVR : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueUIController severeDialogue10;
    [SerializeField] private DialogueUIController severeDialogue11;

    [Header("Optional: Additional Settings")]
    // You can add any additional settings here if needed.

    // Reference to the XRGrabInteractable component.
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Get the XRGrabInteractable component on this syringe.
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("SyringeShotDraggableVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    // No need for OnMouseDown/Drag/Up since XR handles dragging.

    // This method is called when the syringe collides with a target.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NeedleTarget"))
        {
            Debug.Log("SyringeShotDraggableVR: Needle entered target area: " + other.name);

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

            // Optionally, disable further interaction.
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }

            // Disable the syringe (or you could reposition it, etc.).
            gameObject.SetActive(false);
        }
    }
}