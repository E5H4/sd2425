using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DragStripVR : MonoBehaviour
{

    [SerializeField] private GameObject monitorTop;
    [SerializeField] private GameObject monitorTab;

    [SerializeField] private DialogueUIController Monitor10dialoguecontroller;

    [SerializeField] private float insertionThreshold = 0.2f;

    // Reference to the XR Interactable component on this tab.
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Get the XRGrabInteractable component (or your Simple Interactable if it derives from XRBaseInteractable)
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("DragStripVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void Start()
    {
        // Hide the monitor tab UI at start
        if (monitorTab != null)
        {
            monitorTab.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            // Subscribe to the selectExited event: this is called when the user releases the object.
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

    // Called when the user releases the tab from their hand.
    private void OnRelease(SelectExitEventArgs args)
    {
        // Check the distance from the tab to the top of the monitor.
        float distance = Vector3.Distance(transform.position, monitorTop.transform.position);
        Debug.Log("DragStripVR: Distance to monitor top: " + distance);

        if (distance < insertionThreshold)
        {
            Debug.Log("DragStripVR: Tab placed correctly!");
            // Optionally, you can set a flag or enable further interactions here.
            StartCoroutine(HideStripAfterDelay());

            if (Monitor10dialoguecontroller != null)
            {
                Monitor10dialoguecontroller.ShowDialogueUI();
                Debug.Log("DragStripVR: Dialogue triggered!");
            }
        }
        else
        {
            Debug.Log("DragStripVR: Tab not close enough to monitor top.");
        }
    }

    // Coroutine to delay hiding the tab and showing the monitor tab UI.
    private IEnumerator HideStripAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);  // Hide the drag strip itself.
        if (monitorTab != null)
        {
            monitorTab.SetActive(true); // Show the monitor tab UI.
        }
    }
}