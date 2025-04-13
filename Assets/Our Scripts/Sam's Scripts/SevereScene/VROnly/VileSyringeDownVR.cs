using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VileSyringeDownVR : MonoBehaviour
{
    [Header("Dialogue Controllers")]
    [Tooltip("Dialogue 5 controller (will be turned off)")]
    [SerializeField] private DialogueUIController dialogue5Controller;
    [Tooltip("Dialogue 6 controller (will be triggered)")]
    [SerializeField] private DialogueUIController dialogue6Controller;

    [Header("Syringe State Objects")]
    [Tooltip("GameObject representing vilesyringedown (current state)")]
    [SerializeField] private GameObject vilesyringeDown;
    [Tooltip("GameObject representing vilesyringeup (target state)")]
    [SerializeField] private GameObject vilesyringeUp;

    [Header("Rotation Settings")]
    [Tooltip("Target rotation angle (in degrees) for triggering (e.g., 180)")]
    [SerializeField] private float targetRotationAngle = 4f;
    [Tooltip("Tolerance (in degrees) from the target rotation to trigger the effect.")]
    [SerializeField] private float rotationTolerance = 2f;
    [Tooltip("Index of the local rotation axis to check: 0 = X, 1 = Y, 2 = Z")]
    [SerializeField] private int rotationAxis = 0;

    // Reference to the XRGrabInteractable on this object.
    private XRGrabInteractable grabInteractable;

    // Flag to ensure the trigger only fires once.
    private bool hasTriggered = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("VileSyringeRotationTriggerVR: XRGrabInteractable not found on " + gameObject.name);
        }
        else
        {
            // Subscribe to the release event.
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void Update()
    {
        if (hasTriggered)
            return;

        // Get the local Euler angles.
        Vector3 localEuler = transform.localEulerAngles;
        float currentAngle = 0f;
        switch (rotationAxis)
        {
            case 0:
                currentAngle = localEuler.x;
                break;
            case 1:
                currentAngle = localEuler.y;
                break;
            case 2:
                currentAngle = localEuler.z;
                break;
            default:
                currentAngle = localEuler.x;
                break;
        }

        // Calculate the minimal difference between the current angle and the target.
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetRotationAngle));
        if (angleDifference <= rotationTolerance)
        {
            TriggerRotationAction();
        }
    }

    // Logs the current angle when the object is released.
    private void OnReleased(SelectExitEventArgs args)
    {
        Vector3 localEuler = transform.localEulerAngles;
        float currentAngle = 0f;
        switch (rotationAxis)
        {
            case 0:
                currentAngle = localEuler.x;
                break;
            case 1:
                currentAngle = localEuler.y;
                break;
            case 2:
                currentAngle = localEuler.z;
                break;
            default:
                currentAngle = localEuler.x;
                break;
        }

        float difference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetRotationAngle));
        Debug.Log("VileSyringeRotationTriggerVR: Released at angle " + currentAngle +
                  " degrees. Difference to target: " + difference + " degrees.");
    }

    private void TriggerRotationAction()
    {
        hasTriggered = true;
        Debug.Log("VileSyringeRotationTriggerVR: Target rotation reached. Triggering dialogue swap.");

        // Turn off Dialogue 5.
        if (dialogue5Controller != null)
        {
            dialogue5Controller.gameObject.SetActive(false);
        }

        // Activate and show Dialogue 6.
        if (dialogue6Controller != null)
        {
            dialogue6Controller.gameObject.SetActive(true);
            dialogue6Controller.ShowDialogueUI();
        }

        // Switch the visual state.
        if (vilesyringeDown != null)
        {
            vilesyringeDown.SetActive(false);
        }
        if (vilesyringeUp != null)
        {
            vilesyringeUp.SetActive(true);
        }
    }
}