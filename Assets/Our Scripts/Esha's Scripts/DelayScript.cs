using System.Collections;
using UnityEngine;
using DialogueSystemWithText;

public class DelayScript : MonoBehaviour
{
    [Header("the dialogue to play next:")]
    [SerializeField] private DialogueUIController nextDialogueController;
    [SerializeField] private GameObject nextDialogueUI;
    [SerializeField] private float delayInSeconds = 2f;

    public void nextDialogueWithDelay() //attatched to end event of previous dialogue
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (nextDialogueUI != null)
        {
            nextDialogueUI.SetActive(true);
        }

        if (nextDialogueController != null)
        {
            nextDialogueController.ShowDialogueUI();
            Debug.Log("next dialogue triggered after delay.");
        }
        else
        {
            Debug.LogWarning("dialogue controller not assigned.");
        }
    }
}