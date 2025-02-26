using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] private GameObject Headache;  // Reference to the headache stuff
    [SerializeField] private GameObject Dizz;  
    [SerializeField] private GameObject Fatigue; 
    [SerializeField] private GameObject Pale;
    //Open headache object
    public void headache()
    { 
        // Enable headache
        if (Headache != null)
        {
           Headache.SetActive(true);
        }
    }
    public void dizz()
    {
        // Enable Dizzy
        if (Dizz != null)
        {
            Dizz.SetActive(true);
        }
    }

    public void fatigue()
    {
        // Enable fatigue
        if (Fatigue != null)
        {
            Fatigue.SetActive(true);
        }
    }
    public void pale()
    {
        // Enable pale
        if (Pale != null)
        {
            Pale.SetActive(true);
        }
    }

    public void turnOff()
    {
        Headache.SetActive(false);
        Dizz.SetActive(false);
        Fatigue.SetActive(false);
        Pale.SetActive(false);
    }
}
