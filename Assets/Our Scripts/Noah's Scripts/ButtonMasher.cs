using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;
using TMPro;


public class ButtonMasher : MonoBehaviour
{
    public Slider pressureBar;
    public float pressureIncrese = 10f;
    public TextMeshProUGUI endingText;

    private float progression = 0f;
    private bool completion = false;
     private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable pressingAction;

    private void Awake()
    {
       pressingAction = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (pressingAction == null)
        {
            Debug.LogError("BUTTON MASHER: XR Interactable component not found on " + gameObject.name);
        }
    }
    
    private void OnEnable()
    {
         if (pressingAction != null)
        {
            // Subscribe to the selectEntered event
            pressingAction.selectEntered.AddListener(OnButtonMasher);
           
        }
    }

    private void OnDisable()
    {
         if (pressingAction != null)
        {
            pressingAction.selectEntered.RemoveListener(OnButtonMasher);
        }
    }

    private void OnButtonMasher(SelectEnterEventArgs args)
    {
        if(completion) return;

        progression += pressureIncrese;
        progression = Mathf.Clamp(progression, 0f, 100f);
        pressureBar.value = progression;

        if(progression >= 100f)
        {
            completion = true;
            Debug.Log("YOU FINISHED APPLYING PRESSURE!!");
            endingText.gameObject.SetActive(true);
        }
    }
}
