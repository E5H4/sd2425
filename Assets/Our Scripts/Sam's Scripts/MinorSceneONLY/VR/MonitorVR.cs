using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class DragMonitorVR : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject monitorTab;
    [SerializeField] private GameObject monitorTop;
    [SerializeField] private DialogueUIController MinorDialogue3;

    [Header("Threshold Settings")]
    [SerializeField] private float lowerThreshold = .8f;
    [SerializeField] private float upperThreshold = 1f;

    // Reference to the XRGrabInteractable component
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("DragMonitorVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            // Subscribe to the release event.
            grabInteractable.selectExited.AddListener(OnMonitorReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnMonitorReleased);
        }
    }

    // This is called when the user releases the monitor in VR.
    private void OnMonitorReleased(SelectExitEventArgs args)
    {
        // Check if either monitorTab or monitorTop is near the character.
        bool tabNear = IsNearCharacter(monitorTab);
        bool topNear = IsNearCharacter(monitorTop);

        Debug.Log($"DragMonitorVR: Distance - Tab: {GetDistance(monitorTab)}, Top: {GetDistance(monitorTop)}");

        if (tabNear || topNear)
        {
            Debug.Log("DragMonitorVR: Monitor placed correctly!");
            if (MinorDialogue3 != null)
            {
                MinorDialogue3.ShowDialogueUI(); // Trigger the dialogue.
                Debug.Log("DragMonitorVR: Dialogue triggered!");
            }
            StartCoroutine(HideMonitorAfterDelay());
        }
        else
        {
            Debug.Log("DragMonitorVR: Monitor not placed correctly.");
        }
    }

    // Helper to compute distance between an object and the character.
    private float GetDistance(GameObject obj)
    {
        if (obj == null || character == null)
            return float.MaxValue;

        return Vector3.Distance(obj.transform.position, character.transform.position);
    }

    // Returns true if the given object is within the specified thresholds relative to the character.
    private bool IsNearCharacter(GameObject obj)
    {
        float distance = GetDistance(obj);
        return (distance > lowerThreshold && distance < upperThreshold);
    }

    private IEnumerator HideMonitorAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false); // Hide the monitor (or the draggable object) after a short delay.
    }
}