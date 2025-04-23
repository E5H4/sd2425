using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DragLancetVR : MonoBehaviour
{

    [SerializeField] private GameObject characterHand;


    [SerializeField] private DialogueUIController Monitor10dialoguecontroller;
    [SerializeField] private DialogueUIController Monitor11dialoguecontroller;


    [SerializeField] private float lowerThreshold = .8f;
    [SerializeField] private float upperThreshold = 1f;

    // Reference to the XR Grab Interactable component on the lancet.
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("DragLancetVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            // Subscribe to the event when the lancet is released in VR.
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    // Called when the player releases the lancet.
    private void OnReleased(SelectExitEventArgs args)
    {
        if (characterHand == null)
        {
            Debug.LogError("DragLancetVR: Character hand is not assigned.");
            return;
        }

        float distance = Vector3.Distance(transform.position, characterHand.transform.position);
        Debug.Log("DragLancetVR: Distance to character hand: " + distance);

        // Check if the lancet is within the correct range.
        if (distance > lowerThreshold && distance < upperThreshold)
        {
            Debug.Log("DragLancetVR: Lancet placed correctly!");

            if (Monitor11dialoguecontroller != null)
            {
                if (Monitor10dialoguecontroller != null)
                    Monitor10dialoguecontroller.HideDialogueUI();
                Debug.Log("Dialogue Turned Off");
                Monitor11dialoguecontroller.ShowDialogueUI();
                Debug.Log("DragLancetVR: Dialogue triggered!");
            }

            StartCoroutine(HideLancetAfterDelay());
        }
        else
        {
            Debug.Log("DragLancetVR: Lancet not placed correctly.");
        }
    }

    private IEnumerator HideLancetAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}