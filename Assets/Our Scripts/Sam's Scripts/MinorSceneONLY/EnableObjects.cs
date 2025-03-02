using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    [SerializeField] private GameObject monitor;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject pen;
    [SerializeField] private GameObject bottleStrips;
    [SerializeField] private GameObject playerBig;
    [SerializeField] private GameObject characterBig;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject Monitor8Dialogue;
    [SerializeField] private GameObject Monitor9Dialogue;
    [SerializeField] private GameObject Monitor10Dialogue;
    [SerializeField] private GameObject Monitor11Dialogue;

    //So player can't drag items
    public static bool canDragMonitor = false;
    public static bool canDragLancet = false;
    public static bool canDragStrips = false;
    
    
    public void EnableAllObjects() //PLSSSS WORKKKKKKKKK
    {
        if (monitor != null) monitor.SetActive(true);
        else Debug.Log("HELP");
        if (pen != null) pen.SetActive(true);
        if (bottleStrips != null) bottleStrips.SetActive(true);
        if (playerBig != null) playerBig.SetActive(true);
        if (characterBig != null) characterBig.SetActive(true);
        //if (text) text.SetActive(false);

        Debug.Log("Monitor, Pen, and BottleStrips enabled!");
    }


    public void DisableAllObjects() //WORKS
    {
        if (text != null) text.SetActive(false);
        if (player != null) player.SetActive(false);
        if (character != null) character.SetActive(false);

        Debug.Log("Regular characters disabled!");
    }

    public void TurnOffDialogue8()
    {
        if (Monitor8Dialogue != null) Monitor8Dialogue.SetActive(false);
        Debug.Log("Dialogue Turned Off");
    }

    public void TurnOffDialogue9()
    {
        if (Monitor9Dialogue != null) Monitor9Dialogue.SetActive(false);
        Debug.Log("Dialogue Turned Off");
    }
    public void TurnOffDialogue10()
    {
        if (Monitor10Dialogue != null) Monitor10Dialogue.SetActive(false);
        Debug.Log("Dialogue Turned Off");
    }
    public void TurnOffDialogue11()
    {
        if (Monitor11Dialogue != null) Monitor11Dialogue.SetActive(false);
        Debug.Log("Dialogue Turned Off");
    }
}