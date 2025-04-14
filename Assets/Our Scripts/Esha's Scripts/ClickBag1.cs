using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ClickBag1 : MonoBehaviour
{
    [Header("to appear:")]
    [SerializeField] private List<GameObject> objectsToShow = new List<GameObject>();

    private XRBaseInteractable interactable;
    private bool hasActivated = false;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnClicked);
        }
    
    }

    private void OnClicked(SelectEnterEventArgs args)
    {
        if (hasActivated) return;

        foreach (var obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                Debug.Log($"Revealed: {obj.name}");
            }
        }

        hasActivated = true;
        Debug.Log("bag clicked and things appeared");
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnClicked);
        }
    }
}