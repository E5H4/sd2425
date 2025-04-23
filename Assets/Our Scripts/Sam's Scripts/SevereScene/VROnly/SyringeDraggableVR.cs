using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SyringeShotDraggableVR_OneDialogue : MonoBehaviour
{
 
    [SerializeField] private GameObject thighShotF;
    [SerializeField] private GameObject thighShotB;
    [SerializeField] private GameObject armShotF;
    [SerializeField] private GameObject armShotB;

    [SerializeField] private DialogueUIController Severedialogue10;
    [SerializeField] private DialogueUIController Severedialogue11;

    // Reference to the XRGrabInteractable component.
    private XRGrabInteractable grabInteractable;

    // Flag to ensure dialogue is triggered only once.
    private bool dialogueTriggered = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("SyringeShotDraggableVR_OneDialogue: XRGrabInteractable component not found on " + gameObject.name);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (dialogueTriggered)
            return;

        // Determine which shot object to activate based on the collision layer.
        int hitLayer = other.gameObject.layer;
        GameObject shotObj = null;
        if (hitLayer == LayerMask.NameToLayer("thighcolidf"))
        {
            Debug.Log("SyringeShotDraggableVR_OneDialogue: Hit layer 'thighcolidf': " + other.name);
            shotObj = thighShotF;
        }
        else if (hitLayer == LayerMask.NameToLayer("thighcolidb"))
        {
            Debug.Log("SyringeShotDraggableVR_OneDialogue: Hit layer 'thighcolidb': " + other.name);
            shotObj = thighShotB;
        }
        else if (hitLayer == LayerMask.NameToLayer("armcolidf"))
        {
            Debug.Log("SyringeShotDraggableVR_OneDialogue: Hit layer 'armcolidf': " + other.name);
            shotObj = armShotF;
        }
        else if (hitLayer == LayerMask.NameToLayer("armcolidb"))
        {
            Debug.Log("SyringeShotDraggableVR_OneDialogue: Hit layer 'armcolidb': " + other.name);
            shotObj = armShotB;
        }
        else
        {
            // If the collider isn't on one of the expected layers, do nothing.
            return;
        }

        // If we determined a shot object, then activate it.
        if (shotObj != null)
        {
            shotObj.SetActive(true);
        }

        // Trigger the common dialogue.
        if (Severedialogue11 != null)
        {
            Severedialogue10.HideDialogueUI();
            Severedialogue11.ShowDialogueUI();
            Debug.Log("SyringeShotDraggableVR_OneDialogue: Dialogue triggered.");
        }

        dialogueTriggered = true;

        // Disable further grabbing.
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        // Optionally, disable the syringe so it cannot be moved further.
        gameObject.SetActive(false);
    }
}