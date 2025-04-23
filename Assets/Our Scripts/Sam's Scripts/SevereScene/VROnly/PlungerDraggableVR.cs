using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlungerDraggableVR : MonoBehaviour
{
    [Header("Dialogue Controllers")]
    [Tooltip("Dialogue controller triggered when the plunger is pushed down (first press).")]
    [SerializeField] private DialogueUIController severeDialogue7;
    [Tooltip("Dialogue controller triggered when the plunger is pulled up (second press).")]
    [SerializeField] private DialogueUIController severeDialogue10;

    [Header("Plunger State Objects")]
    [Tooltip("GameObject representing the plunger in the up state.")]
    [SerializeField] private GameObject plungerUpObject;
    [Tooltip("GameObject representing the plunger in the down state.")]
    [SerializeField] private GameObject plungerDownObject;

    [SerializeField] private GameObject vilesyringeUp;
    [SerializeField] private GameObject VileShakable;


    [Header("Interaction")]
    [Tooltip("The simple interactable component used for interaction.")]
    [SerializeField] private XRBaseInteractable simpleInteractable;

    

    // Boolean flag: false = currently up; true = currently down.
    private bool pushUp = false;

    private void Awake()
    {
        if (simpleInteractable == null)
        {
            simpleInteractable = GetComponent<XRBaseInteractable>();
            if (simpleInteractable == null)
            {
                Debug.LogError("PlungerToggleBySimpleInteractable: XRBaseInteractable component not found on " + gameObject.name);
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
        if (!pushUp)
        {
            // First press: assume the plunger is up; we want to push it down.
            // Hide the 'up' plunger, enable the 'down' plunger.
            if (plungerUpObject != null)
                plungerUpObject.SetActive(false);
            if (plungerDownObject != null)
                plungerDownObject.SetActive(true);

            pushUp = true;
            Debug.Log("PlungerToggleBySimpleInteractable: Plunger pushed down. Triggering Dialogue 7.");

            // Trigger Dialogue 7.
            if (severeDialogue7 != null)
            {
                severeDialogue7.ShowDialogueUI();
                VileShakable.SetActive(true);
                vilesyringeUp.SetActive(false);


            }
        }
        else
        {
            // Second press: the plunger is down; we want to pull it up.
            if (plungerDownObject != null)
                plungerDownObject.SetActive(false);
            if (plungerUpObject != null)
                plungerUpObject.SetActive(true);

            pushUp = false;
            Debug.Log("PlungerToggleBySimpleInteractable: Plunger pulled up. Triggering Dialogue 10.");

            // Trigger Dialogue 10.
            if (severeDialogue10 != null)
            {
                severeDialogue10.ShowDialogueUI();
            }
        }
    }
}