using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DialogueSystemWithText;

namespace DialogueSystemWithText
{
    [Serializable]
    public class DialogueContent : MonoBehaviour
    {
        [SerializeField, Tooltip("The message of the dialogue.")]
        private string _dialogue = "";
        
        [SerializeField]
        private bool _customizeFont;
        
        [SerializeField]
        private Font _dialogueFont;
        
        [SerializeField]
        private Color _fontColor;
        
        [SerializeField, Tooltip("The name of the character that is speaking in the dialogue. Leaving empty will not show the name in the dialog.")]
        private string _characterName;

        [SerializeField]
        private bool _customizeCharacterName;

        [SerializeField]
        private bool _withLineBreak;
        
        [SerializeField]
        private Color _characterNameFontColor;

        [SerializeField]
        private bool _characterNameFontBold;

        [SerializeField]
        private bool _characterNameFontItalic;

        [SerializeField]
        private int _characterNameFontSize;
        
        [SerializeField]
        private Sprite _characterImageSprite;
        
        [SerializeField]
        private CharacterImagePosition _characterImagePosition;

        [SerializeField]
        private DialogueContent _nextDialogueContent;
        
        [SerializeField]
        private UnityEvent _dialogueStartEvent;
        
        [SerializeField]
        private UnityEvent _dialogueEndEvent;

        [SerializeField, Tooltip("Add to convert dialogue with options.")]
        private List<DialogueOption> _dialogueOptions = new List<DialogueOption>();

        public string Dialogue { get => _dialogue; set => _dialogue = value; }
        public bool CustomizeFont { get => _customizeFont; set => _customizeFont = value; }
        public Font DialogueFont { get => _dialogueFont; set => _dialogueFont = value; }
        public Color FontColor { get => _fontColor; set => _fontColor = value; }
        public Sprite CharacterImageSprite { get => _characterImageSprite; set => _characterImageSprite = value; }
        public CharacterImagePosition CharacterImagePosition { get => _characterImagePosition; set => _characterImagePosition = value; }
        public UnityEvent DialogueStartEvent { get => _dialogueStartEvent; set => _dialogueStartEvent = value; }
        public UnityEvent DialogueEndEvent { get => _dialogueEndEvent; set => _dialogueEndEvent = value; }
        public string CharacterName { get => _characterName; set => _characterName = value; }
        public bool CustomizeCharacterName { get => _customizeCharacterName; set => _customizeCharacterName = value; }
        public Color CharacterNameFontColor { get => _characterNameFontColor; set => _characterNameFontColor = value; }
        public bool CharacterNameFontBold { get => _characterNameFontBold; set => _characterNameFontBold = value; }
        public bool CharacterNameFontItalic { get => _characterNameFontItalic; set => _characterNameFontItalic = value; }
        public int CharacterNameFontSize { get => _characterNameFontSize; set => _characterNameFontSize = value; }
        public DialogueContent NextDialogueContent { get => _nextDialogueContent; set => _nextDialogueContent = value; }
        public List<DialogueOption> DialogueOptions { get => _dialogueOptions; set => _dialogueOptions = value; }
        public bool WithLineBreak { get => _withLineBreak; set => _withLineBreak = value; }

        /// <summary>Method to invoke the methods registered in the dialogue start event.</summary>
        public void InvokeDialogueStartEvent()
        {
            _dialogueStartEvent?.Invoke();
        }

        /// <summary>Method to invoke the methods registered in the dialogue end event.</summary>
        public void InvokeDialogueEndEvent()
        {
            _dialogueEndEvent?.Invoke();
        }
    }

    public enum CharacterImagePosition
    {
        Left, Right
    }
}