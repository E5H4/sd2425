using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AEDTeleport : MonoBehaviour
{
    [SerializeField] private GameObject AED;
    [SerializeField] private Transform teleportedHere;

    // call children to be interactable still
    [SerializeField] private List<GameObject> allowedInteractableChildren;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool hasTeleported = false;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("XR Interactable not found on parent!");
        }
    }

    private void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnClicked);
    }

    private void OnClicked(SelectEnterEventArgs args)
    {
        if (hasTeleported)
        {
            return;
        }

        if (AED != null && teleportedHere != null)
        {
            AED.transform.position = teleportedHere.position;
            AED.transform.rotation = teleportedHere.rotation;
            hasTeleported = true;

            // Disable all child colliders EXCEPT the ones i want
            Collider[] allChildColliders = AED.GetComponentsInChildren<Collider>(true);
            foreach (var col in allChildColliders)
            {
                if (!allowedInteractableChildren.Contains(col.gameObject))
                {
                    col.enabled = false;
                }
            }

            // Disable parent’s own colliders and interaction
            Collider[] parentColliders = GetComponents<Collider>();
            foreach (var col in parentColliders)
            {
                col.enabled = false;
            }

            interactable.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            Debug.Log("AED teleported. this parent isn't clickable anymore.");
        }
    }
}