using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Reflection;

public class PhoneInteraction : MonoBehaviour
{
    [Header("triggered when picked up")]
    [SerializeField] private GameObject DialogueToTrigger;
    [SerializeField] private string methodName = "OnPhonePickup"; // Method name to call

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnPhonePickedUp);
        }
    }

    private void OnPhonePickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Phone picked up!");

        if (DialogueToTrigger != null)
        {
            
            var script = DialogueToTrigger.GetComponent<MonoBehaviour>();

            if (script != null)
            {
                // Get the method info based on method name
                MethodInfo method = script.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

                if (method != null)
                {
                    // Invoke the method
                    method.Invoke(script, null);
                    Debug.Log("Successfully invoked method: " + methodName);
                }
                else
                {
                    Debug.LogError("Method not found: " + methodName);
                }
            }
            else
            {
                Debug.LogError("No MonoBehaviour script found on the assigned GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject assigned to trigger.");
        }

        // prevent repeat triggering?
        grabInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnPhonePickedUp);
        }
    }
}