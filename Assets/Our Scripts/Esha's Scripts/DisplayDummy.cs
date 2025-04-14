using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDummy : MonoBehaviour
{
    [SerializeField] private GameObject dummyWithPads;

    public void ShowDummy()
    {
        if (dummyWithPads != null)
        {
            dummyWithPads.SetActive(true);
            Debug.Log("Dummy with pads is now visible.");
        }
    }

    public void HideDummy()
    {
        if (dummyWithPads != null)
        {
            dummyWithPads.SetActive(false);
            Debug.Log("Dummy with pads is now hidden.");
        }
        else
        {
            Debug.LogWarning("Dummy GameObject is not assigned!");
        }
    }
}