using UnityEngine;
using DialogueSystemWithText;

[RequireComponent(typeof(Collider))]
public class VileSyringDraggable : MonoBehaviour
{

    [SerializeField] private DialogueUIController severeDialogue6;
    [SerializeField] private DialogueUIController severeDialogue8;
    [SerializeField] private DialogueUIController severeDialogue9;


    //Turn on dialogue
    [SerializeField] private GameObject severedialogue6;
    [SerializeField] private GameObject severedialogue8;
    [SerializeField] private GameObject severedialogue9;


    // Define phases of the sequence.
    private enum Phase { RotateUp, Shake, RotateDown }
    private Phase currentPhase = Phase.RotateUp;

    //dragging and double-click rotation.
    private bool isDragging = false;
    private Vector3 dragOffset;
    private Vector3 mouseDownPos;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    private float dragThreshold = 0.1f; 

    // Rotation settings
    [SerializeField] private float rotationIncrement = 45f;
    [SerializeField] private float rotationTolerance = 5f; 

    // Shake mode parameters
    [SerializeField] private float shakeRange = 0.15f;      
    [SerializeField] private float extremeTolerance = 0.02f;  
    [SerializeField] private int requiredShakeCount = 5;
    private int shakeCount = 0;
    private float initialX;        
    private float initialMouseX;   
    private string lastExtreme = "none"; 

    // Internal flag so dialogue is triggered only once per phase.
    private bool dialogueTriggered = false;

    // OnMouseDown: start dragging 
    private void OnMouseDown()
    {
        isDragging = true;
        // Record the initial mouse position (world space) for normal dragging.
        Vector3 mousePos = Input.mousePosition;
        float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = objectZ;
        mouseDownPos = Camera.main.ScreenToWorldPoint(mousePos);
        dragOffset = transform.position - mouseDownPos;

        // If in Shake phase, record initial X positions.
        if (currentPhase == Phase.Shake)
        {
            initialX = transform.localPosition.x;
            initialMouseX = Input.mousePosition.x;
            lastExtreme = "none";
            shakeCount = 0;
        }
    }

    //OnMouseDrag: update dragging
    private void OnMouseDrag()
    {
        if (!isDragging) return;

        if (currentPhase == Phase.Shake)
        {
            //only allow horizontal (x-axis) movement.
            float currentMouseX = Input.mousePosition.x;
            float deltaX = currentMouseX - initialMouseX;

            //convert
            float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 screenPoint0 = new Vector3(0, 0, objectZ);
            Vector3 screenPoint1 = new Vector3(1, 0, objectZ);
            float worldUnitPerPixel = Camera.main.ScreenToWorldPoint(screenPoint1).x -
                                      Camera.main.ScreenToWorldPoint(screenPoint0).x;

            float newLocalX = initialX + deltaX * worldUnitPerPixel;
            // Clamp
            newLocalX = Mathf.Clamp(newLocalX, initialX - shakeRange, initialX + shakeRange);
            Vector3 localPos = transform.localPosition;
            transform.localPosition = new Vector3(newLocalX, localPos.y, localPos.z);

            //Check for shake extremes continuously
            float offsetX = newLocalX - initialX;
            if (offsetX <= -shakeRange + extremeTolerance)
            {
                if (lastExtreme != "left")
                {
                    lastExtreme = "left";
                    shakeCount++;
                    Debug.Log("Shake phase: Left extreme reached. Count: " + shakeCount);
                }
            }
            else if (offsetX >= shakeRange - extremeTolerance)
            {
                if (lastExtreme != "right")
                {
                    lastExtreme = "right";
                    shakeCount++;
                    Debug.Log("Shake phase: Right extreme reached. Count: " + shakeCount);
                }
            }
            else if (Mathf.Abs(offsetX) < extremeTolerance)
            {
                lastExtreme = "none";
            }
        }
        else
        {
            // In Rotate phases, allow full drag
            Vector3 mousePos = Input.mousePosition;
            float objectZ = Camera.main.WorldToScreenPoint(transform.position).z;
            mousePos.z = objectZ;
            Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + dragOffset;
            newPos.z = transform.position.z; // keep z constant
            transform.position = newPos;
        }
    }

    //OnMouseUp: handle click/double-click events
    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        if (currentPhase == Phase.Shake)
        {
            if (shakeCount >= requiredShakeCount && !dialogueTriggered)
            {
                Debug.Log("Shake phase: Shake count reached. Triggering Dialogue 8.");
                severedialogue8.SetActive(true);
                if (severeDialogue8 != null)
                    severeDialogue8.ShowDialogueUI();
                dialogueTriggered = true;
                // Transition to next phase: RotateDown.
                currentPhase = Phase.RotateDown;
                dialogueTriggered = false; // reset for the next phase
            }
        }
        else
        {
            // In Rotate phases, check if the movement was minimal
            Vector3 mouseUpPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(transform.position).z));
            float movement = Vector3.Distance(mouseDownPos, mouseUpPos);

            if (movement < dragThreshold)
            {
                // Check for a double-click.
                if (Time.time - lastClickTime <= doubleClickThreshold)
                {
                    // Rotate the object.
                    transform.Rotate(0, 0, rotationIncrement);
                    Debug.Log("Rotated by " + rotationIncrement + "° due to double-click.");
                    float currentZ = NormalizeAngle(transform.eulerAngles.z);
                    Debug.Log("Current Z rotation = " + currentZ + "°.");

                    // Determine target angle based on phase.
                    float desiredTarget = (currentPhase == Phase.RotateUp) ? 180f : 0f;
                    if (Mathf.Abs(currentZ - desiredTarget) <= rotationTolerance)
                    {
                        if (currentPhase == Phase.RotateUp && !dialogueTriggered)
                        {
                            // When rotating up to 180°, trigger Dialogue 6.
                            Debug.Log("RotateUp: Rotation near 180° achieved. Triggering Dialogue 6.");
                            severedialogue6.SetActive(true);
                            if (severeDialogue6 != null)
                                severeDialogue6.ShowDialogueUI();
                            dialogueTriggered = true;
                            // Transition to Shake phase.
                            currentPhase = Phase.Shake;
                            dialogueTriggered = false; // reset flag for shake phase
                            // Record initial x values for shake mode.
                            initialX = transform.localPosition.x;
                            initialMouseX = Input.mousePosition.x;
                            lastExtreme = "none";
                            shakeCount = 0;
                        }
                        else if (currentPhase == Phase.RotateDown && !dialogueTriggered)
                        {
                            // When rotating down to 0°, trigger Dialogue 9.
                            Debug.Log("RotateDown: Rotation near 0° achieved. Triggering Dialogue 9.");
                            severedialogue9.SetActive(true);
                            if (severeDialogue9 != null)
                                severeDialogue9.ShowDialogueUI();

                            dialogueTriggered = true;
                            // End of sequence: disable further interaction.
                            this.enabled = false;
                        }
                    }
                    lastClickTime = 0f;
                }
                else
                {
                    lastClickTime = Time.time;
                }
            }
        }
    }

    // -------------- Utility: Normalize angle to [0,360) --------------
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0)
            angle += 360f;
        return angle;
    }
}