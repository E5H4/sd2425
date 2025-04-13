using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DialogueSystemWithText;

public class TAPETRIGGER : MonoBehaviour
{
    [SerializeField] GameObject tapeOjectAllowed;
    [SerializeField] GameObject pressureUIVisible;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == tapeOjectAllowed)
        {
            Debug.Log("THIS IS THE TAPE OBJECT!!");
            if(pressureUIVisible != null)
            {
                pressureUIVisible.SetActive(true);
            }
        }
        else{
            Debug.Log("wrong object nothing is happening!!");
        }
    }
}
