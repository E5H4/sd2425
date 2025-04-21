using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUSICBACKGROUND : MonoBehaviour
{
    
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioClip sceneClip; 

    private void Start()
    {
        if (backgroundAudio == null)
        {
            backgroundAudio = GetComponent<AudioSource>();
            if (backgroundAudio == null)
            {
                backgroundAudio = gameObject.AddComponent<AudioSource>();
            }
        }

        if (sceneClip != null)
        {
            backgroundAudio.clip = sceneClip;
            backgroundAudio.loop = false;
            backgroundAudio.volume = 1.0f;
            backgroundAudio.Play();
            Debug.Log("bg music playing");
        }

    }
    
}
