using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeliverablePadsWiresVR : MonoBehaviour
{
    [SerializeField] private AEDPadsChecker padsChecker;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool isHeld = false;
    private bool hasTriggered = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args) => isHeld = true;

    private void OnRelease(SelectExitEventArgs args) => isHeld = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld || hasTriggered || !other.CompareTag("Character")) return;

        padsChecker.RegisterPadHit(gameObject.name);
        hasTriggered = true;
        gameObject.SetActive(false); // hides so that the pad copies appear on victim after
    }
}