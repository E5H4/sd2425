using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDVoicePrompts : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("AED Audio Clips")]
    
    public AudioClip removeClothingClip;
    public AudioClip applyPadsClip;
    public AudioClip analyzingClip;
    public AudioClip shockAdvisedClip;
    public AudioClip noShockClip;
    public AudioClip chargingClip;
    public AudioClip shockDeliveredClip;
    public AudioClip patientStableClip;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayRemoveClothing() 
    {
        PlayClip(removeClothingClip);
    }
    public void PlayApplyPads()
    {
        PlayClip(applyPadsClip);
    }

    public void PlayAnalyzing()
    {
        PlayClip(analyzingClip);
    }

    public void PlayShockAdvised()
    {
        PlayClip(shockAdvisedClip);
    }

    public void PlayNoShock()
    {
        PlayClip(noShockClip);
    }

    public void PlayCharging()
    {
        PlayClip(chargingClip);
    }

    public void PlayShockDelivered()
    {
        PlayClip(shockDeliveredClip);
    }

    public void PlayPatientStable()
    {
        PlayClip(patientStableClip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}