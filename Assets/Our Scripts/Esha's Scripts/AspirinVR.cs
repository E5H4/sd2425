using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class AspirinVR : MonoBehaviour
{
    [Header("Aspirin References")]
    [SerializeField] private GameObject aspirinObject; // The visible aspirin object
    [SerializeField] private DialogueUIController aspirinDialogueController; // Dialogue controller to trigger
    [SerializeField] private GameObject aspirinDialogueUI; // Optional UI GameObject to show (e.g. canvas)

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool isHeld = false;
    private bool hasTriggered = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("AspirinVR: XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("AspirinVR: Aspirin picked up.");

        if (aspirinObject != null)
            aspirinObject.SetActive(true);

        if (aspirinDialogueUI != null)
            aspirinDialogueUI.SetActive(true);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
        Debug.Log("AspirinVR: Aspirin released.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"AspirinVR: Trigger entered by {other.gameObject.name} (tag: {other.tag})");

        if (hasTriggered || !isHeld)
            return;

        if (other.CompareTag("Character"))
        {
            Debug.Log("AspirinVR: Collided with character while held.");

            if (aspirinDialogueController != null)
            {
                aspirinDialogueController.ShowDialogueUI();
            }
            else
            {
                Debug.LogWarning("AspirinVR: Dialogue controller not assigned!");
            }

            hasTriggered = true;
            gameObject.SetActive(false); // Optionally hide the aspirin after use
        }
    }
}