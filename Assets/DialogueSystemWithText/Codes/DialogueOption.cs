using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystemWithText
{
    [Serializable]
    public class DialogueOption
    {
        [SerializeField] private string _option;
        [SerializeField] private DialogueContent _nextDialogueContent;
        [SerializeField] private UnityEvent _optionEvent;

        public string Option { get => _option; set => _option = value; }
        public DialogueContent NextDialogueContent { get => _nextDialogueContent; set => _nextDialogueContent = value; }
        public UnityEvent OptionEvent { get => _optionEvent; set => _optionEvent = value; }
    }
}