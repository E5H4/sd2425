using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class OrangeCapVR : MonoBehaviour
{
    [Header("Dialogue References")]
    [Tooltip("The Dialogue UI Controller to show when the cap is selected.")]
    [SerializeField] private DialogueUIController severeDialogue3;

    [Tooltip("The dialogue GameObject (e.g. a panel) that will be activated when the cap is selected.")]
    [SerializeField] private GameObject severeDialogue3Object;
    [SerializeField] private GameObject NoCap;
    [SerializeField] private GameObject Cap;

    // Flag to prevent multiple activations.
    private bool canClick = true;

    // Reference to the XR Interactable component.
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        // Attempt to get the XR Interactable component.
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("OrangeCapVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event to detect VR interaction.
            interactable.selectEntered.AddListener(OnCapSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnCapSelected);
        }
    }

    // Called when the cap is "pressed" via VR.
    private void OnCapSelected(SelectEnterEventArgs args)
    {
        if (!canClick)
            return;

        Debug.Log("OrangeCapVR: Orange cap selected via VR.");

        if (severeDialogue3 != null)
        {
            severeDialogue3.ShowDialogueUI();
            NoCap.SetActive(true);
            Cap.SetActive(false);

        }

        // Prevent further activations.
        StartCoroutine(DisableOrangeCap());
    }

    private IEnumerator DisableOrangeCap()
    {
        // Optional short delay before disabling.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        canClick = false;
        Debug.Log("OrangeCapVR: Orange cap disabled (set to false).");
    }
}