using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueSystemWithText;

namespace DialogueSystemWithText
{
    public class DialogueOptionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        private DialogueUIController _dialogueUIController;
        private DialogueOption _dialogueOption;

    
        public DialogueUIController DialogueUIController
        {
            get => _dialogueUIController;
            set => _dialogueUIController = value;
        }

        public DialogueOption DialogueOption
        {
            get => _dialogueOption;
            set
            {
                _dialogueOption = value;
                _text.text = _dialogueOption.Option;
                _text.color = Color.white;

            }
        }

        /// <summary>Method which is called when the option button is pressed.</summary>
        public void PressDialogueOptionButton()
        {
            InvokeOptionEvent();
            NextOrHideDialogue();
        }

        /// <summary>Method to invoke the methods registered in the option event.</summary>
        private void InvokeOptionEvent()
        {
            DialogueOption.OptionEvent?.Invoke();
        }

        /// <summary>Method to show next dialogue or hide UI if it is the last dialogue.</summary>
        private void NextOrHideDialogue()
        {
            if(_dialogueOption.NextDialogueContent == null)
            {
                _dialogueUIController.HideDialogueUI();
                return;
            }
            else
            {
                _dialogueUIController.NextDialogue(_dialogueOption.NextDialogueContent);
                return;
            }
        }
    }
}
