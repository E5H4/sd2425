using System.Collections;
using UnityEngine;

public class LayDown : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private AudioClip fallSound; // The audio clip to play when the character lays down
    private AudioSource audioSource;

    private void Awake()
    {
        // Add an AudioSource if not already on the object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // audio settings
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    // delays by 10 seconds to let the dialogue show up and enough time to walk to water behind barista
    public void LayDownCharacterWithDelay()
    {
        StartCoroutine(LayDownCharacterDelayed());
    }

    private IEnumerator LayDownCharacterDelayed()
    {
        yield return new WaitForSeconds(10f);

        // execute the action after the delay
        LayDownCharacter();
    }

    // victim is laid down on back
    public void LayDownCharacter()
    {
        if (character != null)
        {
            character.transform.position = new Vector3(-1.90809393f, 0.27700001f, -1.49624932f);
            character.transform.rotation = Quaternion.Euler(279.999939f, 234.955963f, 73.2450027f);

            // Play fall sound
            if (fallSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fallSound);
            }
            else
            {
                Debug.LogWarning("Fall sound or AudioSource is missing!");
            }
        }
        else
        {
            Debug.LogWarning("Character not assigned in the Inspector!");
        }
    }
}