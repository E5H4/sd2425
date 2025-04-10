using System.Collections;
using UnityEngine;

public class LayDown : MonoBehaviour
{
    [SerializeField] private GameObject character; 

    // delays by 30 seconds to let the dialogue show up and enough time to walk to water behind barista
    public void LayDownCharacterWithDelay() //called first then the laying down action
    {
        
        StartCoroutine(LayDownCharacterDelayed());
    }

    private IEnumerator LayDownCharacterDelayed()
    {
        
        yield return new WaitForSeconds(10f);

        //execute the action after the delay
        LayDownCharacter();
    }

    // victim is laid down on back
    public void LayDownCharacter()
    {
        if (character != null)
        {
        
            character.transform.position = new Vector3(-1.90809393f, 0.27700001f, -1.49624932f);
            character.transform.rotation = Quaternion.Euler(279.999939f, 234.955963f, 73.2450027f);
        }
        else
        {
            Debug.LogWarning("Character not assigned in the Inspector!");
        }
    }
}