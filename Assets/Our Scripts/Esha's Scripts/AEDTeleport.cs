using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AEDTeleport : MonoBehaviour
{
    [SerializeField] private GameObject AED;
    [SerializeField] private Transform teleportedHere;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool hasTeleported = false;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("xr interactable not found!");
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
            Debug.Log("AED already teleported, cant do this again");
            return;
        }

        if (AED != null && teleportedHere != null)
        {
            AED.transform.position = teleportedHere.position;
            AED.transform.rotation = teleportedHere.rotation;
            hasTeleported = true;

            // Disable the interactable so it doesn't block other button clicks
            interactable.enabled = false;

            Debug.Log("AED teleport/interaction TURNED OFF, so no conflicts with inner buttons.");
        }
    }
}