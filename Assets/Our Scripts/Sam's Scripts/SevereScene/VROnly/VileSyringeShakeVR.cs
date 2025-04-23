using UnityEngine;
using DialogueSystemWithText;

public class VileSyringeShakingVR : MonoBehaviour
{
    [Header("Dialogue Controllers")]
    [Tooltip("Dialogue 7 controller (will be turned off after shaking).")]
    [SerializeField] private DialogueUIController severeDialogue7;
    [Tooltip("Dialogue 8 controller (activated once shaking is completed).")]
    [SerializeField] private DialogueUIController severeDialogue8;
    [Tooltip("Dialogue 9 controller (activated after rotation is confirmed).")]
    [SerializeField] private DialogueUIController severeDialogue9;

    [Header("External Object")]
    [Tooltip("The VileSyringePlunger object to activate once rotation is reached.")]
    [SerializeField] private GameObject VileSyringePlunger;

    [Header("Rotation Settings")]
    [Tooltip("Target rotation angle (in degrees) for triggering Dialogue 9 (e.g., 180).")]
    [SerializeField] private float targetRotationAngle = 4f;
    [Tooltip("Tolerance (in degrees) from the target rotation that will trigger Dialogue 9.")]
    [SerializeField] private float rotationTolerance = 2f;
    [Tooltip("Local rotation axis to check: 0 = X, 1 = Y, 2 = Z.")]
    [SerializeField] private int rotationAxis = 0;

    [Header("Shake Detection Settings")]
    [Tooltip("Movement threshold (in world units per frame) to register as a shake.")]
    [SerializeField] private float shakeMovementThreshold = 0.1f;
    [Tooltip("Number of shakes required before triggering Dialogue 8.")]
    [SerializeField] private int requiredShakeCount = 5;

    // Internal state for shake detection.
    private int shakeCount = 0;
    private bool shakeCompleted = false;
    private bool rotationTriggered = false;

    // Manual tracking of the object's position.
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        // --- Shake detection section ---
        if (!shakeCompleted)
        {
            Vector3 currentPosition = transform.position;
            // Compute the movement (distance) between frames.
            float movementThisFrame = Vector3.Distance(currentPosition, lastPosition);
            Debug.Log("Movement this frame: " + movementThisFrame);

            if (movementThisFrame > shakeMovementThreshold)
            {
                shakeCount++;
                Debug.Log("Shake detected! Count = " + shakeCount);
            }

            lastPosition = currentPosition;

            if (shakeCount >= requiredShakeCount)
            {
                shakeCompleted = true;
                Debug.Log("Shake complete: Disabling Dialogue 7, enabling Dialogue 8.");
                if (severeDialogue7 != null)
                {
                    severeDialogue7.HideDialogueUI();
                }
                if (severeDialogue8 != null)
                {
                    severeDialogue8.ShowDialogueUI();
                }
            }
        }

        // --- Rotation check section ---
        if (shakeCompleted && !rotationTriggered)
        {
            // Get the local Euler angles.
            Vector3 localEuler = transform.localEulerAngles;
            float currentAngle = 0f;
            switch (rotationAxis)
            {
                case 0: currentAngle = localEuler.x; break;
                case 1: currentAngle = localEuler.y; break;
                case 2: currentAngle = localEuler.z; break;
            }

            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetRotationAngle));
            Debug.Log("Current rotation: " + currentAngle + "°, difference: " + angleDifference + "°");
            if (angleDifference <= rotationTolerance)
            {
                rotationTriggered = true;
                Debug.Log("Target rotation reached. Turning off Dialogue 8 and triggering Dialogue 9.");
                if (severeDialogue8 != null)
                {
                    severeDialogue8.HideDialogueUI();
                }
                if (severeDialogue9 != null)
                {
                    severeDialogue9.ShowDialogueUI();
                }
                if (VileSyringePlunger != null)
                {
                    VileSyringePlunger.SetActive(true);
                }
                // Finally, disable this object.
                gameObject.SetActive(false);
            }
        }
    }
}