using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAspirin : MonoBehaviour
{
    [SerializeField] private GameObject aspirin;

    public void ShowAspirin()
    {
        if (aspirin != null)
        {
            aspirin.SetActive(true);
            Debug.Log("aspirin visible");
        }
        else
        {
            Debug.LogWarning("Aspirin GameObject is not assigned!");
        }
    }

}