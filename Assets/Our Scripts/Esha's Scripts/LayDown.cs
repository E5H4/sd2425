using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayDown : MonoBehaviour
{
    [SerializeField] private GameObject character; 

    // Method to lay down the character when triggered
    public void LayDownCharacter()
    {
        if (character != null)
        {
            // on her back
            character.transform.position = new Vector3(-1.90809393f, 0.27700001f, -1.49624932f);
            character.transform.rotation = Quaternion.Euler(279.999939f, 234.955963f, 73.2450027f);
        }
        else
        {
            Debug.LogWarning("Character not assigned in the Inspector!");
        }
    }
}