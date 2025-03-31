using UnityEngine;
using DialogueSystemWithText;

[RequireComponent(typeof(Collider))]
public class PlungerDraggable : MonoBehaviour
{ 
    [SerializeField] private DialogueUIController severeDialogue7;
    [SerializeField] private DialogueUIController severeDialogue10;

    [SerializeField] private GameObject severedialogue7;
    [SerializeField] private GameObject severedialogue10;

    // false = push‑in mode (default)
    // true = pull‑out mode
    [SerializeField] private bool pullOutMode = false;

    //push‑in mode
    [SerializeField] private float pushInTopY = -0.0256694f;   // Fully pushed in
    [SerializeField] private float pushInBottomY = -0.08549959f;
    [SerializeField] private float pushInTriggerY = -0.0256694f;  // When near this, trigger Dialogue 7
    [SerializeField] private float pushInTolerance = 0.001f;

    //pull‑out mode
    [SerializeField] private float pullOutStartY = -0.0188f;      // Starting local Y when switching to pull‑out
    [SerializeField] private float pullOutBottomY = -0.08549959f;   // Fully pulled out
    [SerializeField] private float pullOutTriggerY = -0.08549959f;  // When near this, trigger Dialogue 10
    [SerializeField] private float pullOutTolerance = 0.001f;

    //convert mouse Y movement (in pixels) to a change in local Y.
    [SerializeField] private float dragScale = 0.001f;
    // Separate drag scale for pull‑out mode if needed.
    [SerializeField] private float pullOutDragScale = 0.8f;

    //tracking drag.
    private bool isDragging = false;
    private float initialLocalY;
    private float initialMouseY;

    // Fixed local
    private float fixedLocalX;
    private float fixedLocalZ;

  
    private bool dialogueTriggered = false;


    private void Start()
    {
        // Record the starting local position.
        Vector3 localPos = transform.localPosition;
        fixedLocalX = localPos.x;
        fixedLocalZ = localPos.z;
        initialLocalY = localPos.y;
    }

    //OnMouseDown
    private void OnMouseDown()
    {
        isDragging = true;
        // Update fixed x and z from current position.
        fixedLocalX = transform.localPosition.x;
        fixedLocalZ = transform.localPosition.z;
        // Record initial mouse Y and local Y.
        initialMouseY = Input.mousePosition.y;
        if (pullOutMode)
        {
            //force the starting position to pullOutStartY.
            initialLocalY = pullOutStartY;
            transform.localPosition = new Vector3(fixedLocalX, pullOutStartY, fixedLocalZ);
            dialogueTriggered = false;
        }
        else
        {
            //use the current local Y.
            initialLocalY = transform.localPosition.y;
        }
    }

    //OnMouseDrag
    private void OnMouseDrag()
    {
        if (!isDragging) return;

        float currentMouseY = Input.mousePosition.y;
        float deltaLocalY = 0f;

        if (!pullOutMode)
        {
            // Dragging down: currentMouseY decreases -> initialMouseY - currentMouseY is positive, which increases local Y.
            float deltaMouse = initialMouseY - currentMouseY;
            deltaLocalY = deltaMouse * dragScale;
        }
        else
        {
            //compute delta as (initialMouseY - currentMouseY) (positive when dragging down), then invert it so that local Y decreases.
            float deltaMouse = initialMouseY - currentMouseY;
            deltaLocalY = -deltaMouse * pullOutDragScale;
            Debug.Log("Pull‑out mode: deltaMouse = " + deltaMouse + ", deltaLocalY = " + deltaLocalY);
        }

        float newLocalY = initialLocalY + deltaLocalY;

        if (!pullOutMode)
        {
            // Clamp for push‑in mode.
            newLocalY = Mathf.Clamp(newLocalY, pushInBottomY, pushInTopY);
        }
        else
        {
            // Clamp for pull‑out mode.
            newLocalY = Mathf.Clamp(newLocalY, pullOutBottomY, pullOutStartY);
        }

        transform.localPosition = new Vector3(fixedLocalX, newLocalY, fixedLocalZ);
    }

    //OnMouseUp
    private void OnMouseUp()
    {
        isDragging = false;
        if (!pullOutMode)
        {
            //if plunger is near the top (pushed in), trigger Dialogue 7.
            if (!dialogueTriggered && Mathf.Abs(transform.localPosition.y - pushInTriggerY) < pushInTolerance)
            {
                Debug.Log("PlungerDraggable: Push‑in trigger reached. Triggering Dialogue 7.");
                severedialogue7.SetActive(true);
                if (severeDialogue7 != null)
                    severeDialogue7.ShowDialogueUI();
                dialogueTriggered = true;
                pullOutMode = true;
            }
        }
        else
        {
            //if plunger is near the bottom, trigger Dialogue 10.
            if (!dialogueTriggered && Mathf.Abs(transform.localPosition.y - pullOutTriggerY) < pullOutTolerance)
            {
                Debug.Log("PlungerDraggable: Pull‑out trigger reached. Triggering Dialogue 10.");
                severedialogue10.SetActive(true);
                if (severeDialogue10 != null)
                    severeDialogue10.ShowDialogueUI();
                dialogueTriggered = true;
            }
        }
    }
}