using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] private GameObject Headache;  // Reference to the headache stuff
    [SerializeField] private GameObject Dizz;  
    [SerializeField] private GameObject Fatigue; 
    [SerializeField] private GameObject Pale;
    [SerializeField] private GameObject MonitorImage;
    [SerializeField] private GameObject Monitor;  // Reference to the headache stuff
    [SerializeField] private GameObject BoxStrips;
    [SerializeField] private GameObject Strips;
    [SerializeField] private GameObject Lancet;
    //Open headache object
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
        MonitorImage.SetActive(false);
        Monitor.SetActive(false);
        BoxStrips.SetActive(false);
        Strips.SetActive(false);
        Lancet.SetActive(false);

    }

    public void monitorimage()
    {
        // Enable Monitor
        if (MonitorImage != null)
        {
            MonitorImage.SetActive(true);
        }
    }

    public void monitor()
    {
        // Enable Monitor
        if (Monitor != null)
        {
            Monitor.SetActive(true);
        }
    }

    public void boxstrips()
    {
        // Enable boxstrips
        if (BoxStrips != null)
        {
            BoxStrips.SetActive(true);
        }
    }

    public void strips()
    {
        // Enable strips
        if (Strips != null)
        {
            Strips.SetActive(true);
        }
    }

    public void lancet()
    {
        // Enable lancets
        if (Lancet != null)
        {
            Lancet.SetActive(true);
        }
    }
}
