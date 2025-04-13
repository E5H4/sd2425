using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlungerPullUpVR : MonoBehaviour
{
    [Header("Dialogue Controllers")]
    [Tooltip("The dialogue controller to turn OFF (Dialogue 9).")]
    [SerializeField] private DialogueUIController severeDialogue9;
    [Tooltip("The dialogue controller to trigger (Dialogue 10).")]
    [SerializeField] private DialogueUIController severeDialogue10;


    [Header("Interaction")]
    [Tooltip("The XR Base Interactable used to detect a press in VR.")]
    [SerializeField] private XRBaseInteractable simpleInteractable;

    private void Awake()
    {
        if (simpleInteractable == null)
        {
            simpleInteractable = GetComponent<XRBaseInteractable>();
            if (simpleInteractable == null)
            {
                Debug.LogError("PlungerPullUpVR: XRBaseInteractable component not found on " + gameObject.name);
            }
        }
    }

    private void OnEnable()
    {
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnPlungerPressed);
        }
    }

    private void OnDisable()
    {
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.RemoveListener(OnPlungerPressed);
        }
    }

    private void OnPlungerPressed(SelectEnterEventArgs args)
    {
        Debug.Log("PlungerPullUpVR: Plunger pressed for pull-up action.");

        // Turn off Dialogue 9.
        if (severeDialogue9 != null)
        {
            severeDialogue9.gameObject.SetActive(false);
        }

        // Activate and show Dialogue 10.
        if (severeDialogue10 != null)
        {
            severeDialogue10.gameObject.SetActive(true);
            severeDialogue10.ShowDialogueUI();
        }

        // Turn off the VileSyringePlunger object.
    
            gameObject.SetActive(false);

    }
}