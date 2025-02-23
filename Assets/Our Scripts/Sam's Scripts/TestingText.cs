using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TestingText : MonoBehaviour
{
    [SerializeField] private DialogueUIController _dialogueUIController;
    // Start is called before the first frame update
    void Start()
    { 
        _dialogueUIController.ShowDialogueUI();
    }
}
