using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class BookbagVR : MonoBehaviour
{
    [SerializeField] public GameObject bag;
    [SerializeField] public GameObject soda;
    [SerializeField] public GameObject sodaVR;
    public static bool minorsecond = false;
    private int count = 0;


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
        count++;

        if (Difficulty.difficulty=="Minor" && count==2)
        {
            minorsecond = true;
            sodaVR.SetActive(true);
            soda.SetActive(false);

        }
        if (bag != null && TestingText.bookbagButton)
        {
            bag.SetActive(true);
        }
    }
}