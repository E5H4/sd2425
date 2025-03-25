using System.Collections;
using UnityEngine;
using DialogueSystemWithText;

[RequireComponent(typeof(Collider))]
public class OrangeCapDraggable : MonoBehaviour
{
    // Dialogue controller for step one.
    [SerializeField] private DialogueUIController severeDialogue3;

    // Flag to prevent multiple clicks.
    private bool canClick = true;

    private void OnMouseDown()
    {
        if (canClick)
        {
            Debug.Log("OrangeCapDraggable: Orange cap clicked, triggering dialogue.");
            if (severeDialogue3 != null)
            {
                severeDialogue3.ShowDialogueUI();
            }
            // Disable further interaction by starting the coroutine.
            StartCoroutine(DisableOrangeCap());
        }
    }

    private IEnumerator DisableOrangeCap()
    {
        // Optional short delay before disabling.
        yield return new WaitForSeconds(0.1f);
        // Disable the object, effectively setting "orangecap" to false.
        gameObject.SetActive(false);
        canClick = false;
        Debug.Log("OrangeCapDraggable: Orange cap disabled (set to false).");
    }
}