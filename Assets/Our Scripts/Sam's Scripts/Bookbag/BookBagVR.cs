using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class BookbagVR : MonoBehaviour
{
    [SerializeField] public GameObject bag;
    

    // Reference to the XR Interactable component 
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("BookbagVR: XR Interactable component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            // Subscribe to the selectEntered event
            interactable.selectEntered.AddListener(OnBagSelected);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnBagSelected);
        }
    }

    // This is called when the backpack is activated via VR
    private void OnBagSelected(SelectEnterEventArgs args)
    {
        Debug.Log("BookbagVR: Backpack selected via VR.");
        if (bag != null)
        {
            bag.SetActive(true);
        }
    }
    }