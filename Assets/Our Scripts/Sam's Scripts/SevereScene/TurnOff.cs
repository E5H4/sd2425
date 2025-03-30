using UnityEngine;

public class TurnOff : MonoBehaviour
{
    [SerializeField] private GameObject SevereDialogue3;
    [SerializeField] private GameObject SevereDialogue4;
    [SerializeField] private GameObject SevereDialogue5;
    [SerializeField] private GameObject SevereDialogue6;
    [SerializeField] private GameObject SevereDialogue7;
    [SerializeField] private GameObject SevereDialogue8;
    [SerializeField] private GameObject SevereDialogue9;

    public void TurnOffDialogue3()
    {
        if (SevereDialogue3 != null) SevereDialogue3.SetActive(false);
        Debug.Log("Dialogue 3 turned off");
    }

    public void TurnOffDialogue4()
    {
        if (SevereDialogue4 != null) SevereDialogue4.SetActive(false);
        Debug.Log("Dialogue 4 turned off");
    }

    public void TurnOffDialogue5()
    {
        if (SevereDialogue5 != null) SevereDialogue5.SetActive(false);
        Debug.Log("Dialogue 5 turned off");
    }

    public void TurnOffDialogue6()
    {
        if (SevereDialogue6 != null) SevereDialogue6.SetActive(false);
        Debug.Log("Dialogue 6 turned off");
    }

    public void TurnOffDialogue7()
    {
        if (SevereDialogue7 != null) SevereDialogue7.SetActive(false);
        Debug.Log("Dialogue 7 turned off");
    }

    public void TurnOffDialogue8()
    {
        if (SevereDialogue8 != null) SevereDialogue8.SetActive(false);
        Debug.Log("Dialogue 8 turned off");
    }

    public void TurnOffDialogue9()
    {
        if (SevereDialogue9 != null) SevereDialogue9.SetActive(false);
        Debug.Log("Dialogue 9 turned off");
    }

    public void turnoffdialogue()
    {
        TurnOffDialogue3();
        TurnOffDialogue4();
        TurnOffDialogue5();
        TurnOffDialogue6();
        TurnOffDialogue7();
        TurnOffDialogue8();
        TurnOffDialogue9();
    }
}