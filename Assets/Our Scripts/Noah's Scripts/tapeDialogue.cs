using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class tapeDialogue : MonoBehaviour
{
      [SerializeField] private AudioSource tapeSource;
   [SerializeField] private AudioClip tapeClip; // sound that plays on click

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable action;

    private void Awake()
    {
        action = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (action == null)
        {
            Debug.LogError("PhoneVR: XR Interactable component not found on " + gameObject.name);
        }


         if (tapeSource == null)
        {
            tapeSource = GetComponent<AudioSource>();
            if (tapeSource == null)
            {
                tapeSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnEnable()
    {
        if (action != null)
        {
            action.selectEntered.AddListener(tapeAudio);
        }
    }
    
    private void OnDisable()
    {
        if (action != null)
        {
            action.selectEntered.RemoveListener(tapeAudio);
        }
    }

    private void tapeAudio(SelectEnterEventArgs args)
    {
        if(tapeSource != null && tapeClip != null)
        {
            tapeSource.clip = tapeClip;
            tapeSource.loop = false;
            tapeSource.Play();
            Debug.Log("IM BEING PLAYED ON CLICK SIR");
        }
    }
}
