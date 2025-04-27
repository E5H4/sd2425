using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class SyringeVRDraggable : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueUIController severeDialogue4;
    [SerializeField] private DialogueUIController severeDialogue5;  // Dialogue controller to trigger.

    [Header("Vile References")]
    [SerializeField] private Transform vileTransform;    // General position reference for the vile.
    [SerializeField] private Transform vileTopTransform; // Reference for the top of the vile.
    [SerializeField] private GameObject bottleSyringeObject; // Combined object (syringe + bottle) to activate.

    // Distance thresholds
    [SerializeField] private float insertionThreshold = 0.55f;
    [SerializeField] private float topThreshold = 0.1f;

    // XR Grab interactable reference for VR dragging
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Get the XRGrabInteractable component on this object.
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("SyringeVRDraggable: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            // Listen for the event when the syringe is released.
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    // This is called when the player releases the syringe (i.e. stops grabbing it).
    private void OnRelease(SelectExitEventArgs args)
    {
        // Compute the distance from the syringe to the vile.
        float distance = Vector3.Distance(transform.position, vileTransform.position);
        Debug.Log("SyringeVRDraggable: Distance to vile: " + distance);

        // Check that the syringe is close enough to the vile AND that its "needle" is at the top.
        if (distance < insertionThreshold && IsNeedleAtTop())
        {
            Debug.Log("SyringeVRDraggable: Needle is at the top of the vile. Triggering dialogue and activating combined object.");

            if (severeDialogue5 != null)
            {
                severeDialogue4.HideDialogueUI();
                severeDialogue5.ShowDialogueUI();
            }
            if (bottleSyringeObject != null)
            {
                bottleSyringeObject.SetActive(true);
            }
            // Optionally disable the syringe and the vile.
            gameObject.SetActive(false);
            if (vileTransform != null)
            {
                vileTransform.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("SyringeVRDraggable: Needle is not at the top of the vile.");
        }
    }

    // Check whether the syringe's current position is near the top of the vile.
    private bool IsNeedleAtTop()
    {
        if (vileTopTransform == null)
        {
            Debug.LogWarning("SyringeVRDraggable: Vile top transform is not assigned.");
            return false;
        }
        float topDistance = Vector3.Distance(transform.position, vileTopTransform.position);
        Debug.Log("SyringeVRDraggable: Distance to vile top: " + topDistance);
        return topDistance < topThreshold;
    }
}