using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ==========================================================
// License Information
// ==========================================================
// °•ABOUT•°
// • Artist: TXT (투모로우바이투게더)
// • Music: Our Summer
// • Album: The Dream Chapter: STAR
// • Release Date: 2019.05.31
// • Official Audio: https://youtu.be/eo_NXbXbzsA?si=iCKVVH1tqxyvyXLp
// Source:
//     • TXT(투모로우바이투게더) - Our Summer (piano cover)
// License: Creative Commons Attribution (https://creativecommons.org/licenses/...)

public class BgMusic : MonoBehaviour
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
            backgroundAudio.loop = true;
            backgroundAudio.volume = 0.064f;
            backgroundAudio.Play();
            Debug.Log("bg music playing");
        }

    }
}