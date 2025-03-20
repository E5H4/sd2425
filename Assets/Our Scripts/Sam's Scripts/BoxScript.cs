using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] private GameObject Symptom1;  // Headache and Confusion
    [SerializeField] private GameObject Symptom2;  //Dizz and coord
    [SerializeField] private GameObject Symptom3; //fatigue and speech
    [SerializeField] private GameObject Symptom4; //pale and blurr

    //for minor scene
    [SerializeField] private GameObject MonitorImage;
    [SerializeField] private GameObject Monitor;  
    [SerializeField] private GameObject BoxStrips;
    [SerializeField] private GameObject Strips;
    [SerializeField] private GameObject Lancet;

    //for severe scene
    [SerializeField] private GameObject NeedlesImage;
    [SerializeField] private GameObject VialImage;
    [SerializeField] private GameObject Plunger;
    [SerializeField] private GameObject Needle;
    [SerializeField] private GameObject Syringe;
    [SerializeField] private GameObject Cap;
    [SerializeField] private GameObject Powder;
    [SerializeField] private GameObject Step1;
    [SerializeField] private GameObject Step2;
    [SerializeField] private GameObject Step3;
    [SerializeField] private GameObject Step4;



    //Open objects
    public void symptom1()
    { 
        // Enable headache
        if (Symptom1 != null)
        {
            Symptom1.SetActive(true);
        }
    }
    public void symptom2()
    {
        // Enable Dizzy
        if (Symptom2 != null)
        {
            Symptom2.SetActive(true);
        }
    }

    public void symtpom3()
    {
        // Enable fatigue
        if (Symptom3 != null)
        {
            Symptom3.SetActive(true);
        }
    }
    public void symptom4()
    {
        // Enable pale
        if (Symptom4 != null)
        {
            Symptom4.SetActive(true);
        }
    }



    public void turnOff()
    {
        Symptom1.SetActive(false);
        Symptom2.SetActive(false);
        Symptom3.SetActive(false);
        Symptom4.SetActive(false);
        MonitorImage.SetActive(false);
        Monitor.SetActive(false);
        BoxStrips.SetActive(false);
        Strips.SetActive(false);
        Lancet.SetActive(false);
        NeedlesImage.SetActive(false);
        VialImage.SetActive(false);
        Plunger.SetActive(false);
        Needle.SetActive(false);
        Syringe.SetActive(false);
        Cap.SetActive(false);
        Powder.SetActive(false);
        Step1.SetActive(false);
        Step2.SetActive(false);
        Step3.SetActive(false);
        Step4.SetActive(false);
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
    public void needlesimage()
    {
        // Enable Monitor
        if (NeedlesImage != null)
        {
            NeedlesImage.SetActive(true);
        }
    }

    public void vialimage()
    {
        // Enable Monitor
        if (VialImage != null)
        {
            VialImage.SetActive(true);
        }
    }

    public void plunger()
    {
        // Enable Monitor
        if (Plunger != null)
        {
            Plunger.SetActive(true);
        }
    }

    public void needles()
    {
        // Enable boxstrips
        if (Needle != null)
        {
            Needle.SetActive(true);
        }
    }

    public void syringe()
    {
        // Enable strips
        if (Syringe != null)
        {
            Syringe.SetActive(true);
        }
    }

    public void cap()
    {
        // Enable lancets
        if (Cap != null)
        {
            Cap.SetActive(true);
        }
    }

    public void powder()
    {
        // Enable lancets
        if (Powder != null)
        {
            Powder.SetActive(true);
        }
    }

    public void step1()
    {
        // Enable lancets
        if (Step1 != null)
        {
            Step1.SetActive(true);
        }
    }

    public void step2()
    {
        // Enable lancets
        if (Step2 != null)
        {
            Step2.SetActive(true);
        }
    }

    public void step3()
    {
        // Enable lancets
        if (Step3 != null)
        {
            Step3.SetActive(true);
        }
    }

    public void step4()
    {
        // Enable lancets
        if (Step4 != null)
        {
            Step4.SetActive(true);
        }
    }




}

