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
     private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable pressingAction;

    private void Awake()
    {
       pressingAction = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (pressingAction == null)
        {
            Debug.LogError("BUTTON MASHER: XR Interactable component not found on " + gameObject.name);
        }
        else
        {
            Debug.Log("the action isnt registering and is null");
        }
    }
    
    private void OnEnable()
    {
        Debug.Log("ENABLING IS HAPPENING!");
         if (pressingAction != null)
        {
            // Subscribe to the selectEntered event
            pressingAction.selectEntered.AddListener(OnButtonMasher);
           Debug.Log("Add to selectentered events");
        }
        else
        {
            Debug.Log("ITS NULL IN ENABLE!");
        }
    }

    private void OnDisable()
    {
        Debug.Log("DISABLING IS HAPPENING!!");
         if (pressingAction != null)
        {
            pressingAction.selectEntered.RemoveListener(OnButtonMasher);
            Debug.Log("YOU HAVE BEEN REMOVED!");
        }
       
    }

    public void OnButtonMasher(SelectEnterEventArgs args)
    {
        Debug.Log("This interaction is being triggers from the interaction");
        if(completion)
        {
             Debug.Log("INTERACTION HAS BEEN IGNORED, YOU'VE COMPLETED THE PRESSURE");
             return;
             
        }

        progression += pressureIncrese;
        progression = Mathf.Clamp(progression, 0f, 100f);
        pressureBar.value = progression;
        Debug.Log("I AM INCREASING THE PRESSURE: {progression}");
        if(progression >= 100f)
        {
            completion = true;
            Debug.Log("YOU FINISHED APPLYING PRESSURE!!");
            if(endingText != null)
            {
                endingText.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("THIS HASNT BEEN ASSIGNED OH NOOO!!");
            }
        }
    }
}
