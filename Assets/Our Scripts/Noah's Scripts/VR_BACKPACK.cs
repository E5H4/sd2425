using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;


public class VR_BACKPACK : MonoBehaviour
{
    
    [SerializeField] public GameObject backPackVR;
    [SerializeField] private AudioSource backpackAudioSource;
    [SerializeField] private AudioClip backpackClip;
    

    // This is a reference to the XR Interactable component 
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable action;

    private void Awake()
    {
        action = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (action == null)
        {
            Debug.LogError("BookbagVR: XR Interactable component not found on " + gameObject.name);
        }

        if (backpackAudioSource == null)
        {
            backpackAudioSource = GetComponent<AudioSource>();
            if (backpackAudioSource == null)
            {
                backpackAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnEnable()
    {
        if (action != null)
        {
            // Subscribe to the selectEntered event
            action.selectEntered.AddListener(BagSelection);
           
        }
    }

    private void OnDisable()
    {
        if (action != null)
        {
            action.selectEntered.RemoveListener(BagSelection);
        }
    }

    // This is called when the backpack is activated via VR
    private void BagSelection(SelectEnterEventArgs args)
    {
        Debug.Log("VR BACKPACK HAS BEEN SELECTED GOOD SIR!");
        if (backPackVR != null)
        {
            backPackVR.SetActive(true);
        }
        if (backpackAudioSource != null && backpackClip != null)
        {
            backpackAudioSource.clip = backpackClip;
            backpackAudioSource.loop = false; // Only play's once
            backpackAudioSource.volume = 1.0f; 
            backpackAudioSource.Play();
        }
    }
    
}
