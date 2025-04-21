using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class sodaDialogue : MonoBehaviour
{
    [SerializeField] private AudioSource sodaSource;
   [SerializeField] private AudioClip sodaClip; // sound that plays on click

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable action;

    private void Awake()
    {
        action = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (action == null)
        {
            Debug.LogError("PhoneVR: XR Interactable component not found on " + gameObject.name);
        }


         if (sodaSource == null)
        {
            sodaSource = GetComponent<AudioSource>();
            if (sodaSource == null)
            {
                sodaSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnEnable()
    {
        if (action != null)
        {
            action.selectEntered.AddListener(sodaAudio);
        }
    }
    
    private void OnDisable()
    {
        if (action != null)
        {
            action.selectEntered.RemoveListener(sodaAudio);
        }
    }

    private void sodaAudio(SelectEnterEventArgs args)
    {
        if(sodaSource != null && sodaClip != null)
        {
            sodaSource.clip = sodaClip;
            sodaSource.loop = false;
            sodaSource.Play();
            Debug.Log("IM BEING PLAYED ON CLICK SIR");
        }
    }
}
