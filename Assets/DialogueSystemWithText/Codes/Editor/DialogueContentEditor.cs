using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;

namespace DialogueSystemWithText
{
    [CustomEditor(typeof(DialogueContent))]
    public class DialogueContentEditor : Editor
    {
        private Vector2 _scroll;
        private SerializedProperty _dialogueStartEvent;
        private SerializedProperty _dialogueEndEvent;
        private SerializedProperty _dialogueOptions;

        private void OnEnable()
        {
            _dialogueStartEvent = serializedObject.FindProperty("_dialogueStartEvent");
            _dialogueEndEvent = serializedObject.FindProperty("_dialogueEndEvent");
            _dialogueOptions = serializedObject.FindProperty("_dialogueOptions");
        }

        public override void OnInspectorGUI()
        {
            DialogueContent dialogueContent = (DialogueContent)target;

            GUIContent dialogueGUIContent = new GUIContent("Dialogue", "The dialog text to display.");
            EditorGUILayout.LabelField(dialogueGUIContent);
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            dialogueContent.Dialogue = EditorGUILayout.TextArea(dialogueContent.Dialogue, GUILayout.Height(50));
            EditorGUILayout.EndScrollView();
            if(dialogueContent.Dialogue == "")
            {
                EditorGUILayout.HelpBox("The Dialogue is empty. Please write the dialogue that he wants to display.", MessageType.Warning);
                dialogueContent.CustomizeFont = false;
            }
            else
            {
                GUIContent customizeDialogueFontGUIContent = new GUIContent("Customize Dialogue Font", "Allows to modify the font for this dialogue.");
                dialogueContent.CustomizeFont = EditorGUILayout.Toggle(customizeDialogueFontGUIContent, dialogueContent.CustomizeFont);
            }



            if(dialogueContent.CustomizeFont)
            {
                dialogueContent.DialogueFont = (Font)EditorGUILayout.ObjectField("Dialogue Font", dialogueContent.DialogueFont, typeof(Font), true);
                dialogueContent.FontColor = EditorGUILayout.ColorField("Font Color", dialogueContent.FontColor);
                if(dialogueContent.FontColor.a == 0) {
                    EditorGUILayout.HelpBox("Warning: Be careful the color is transparent. Modify the alpha.", MessageType.Warning);
                    Debug.LogWarning("Warning: Be careful, the Font Color of the DialogueContent script is transparent. Modify the alpha.");
                }
            }

            GUIContent characterNameGUIContent = new GUIContent("Character Name", "The name of the character that is speaking in the dialogue. Leaving empty will not show the name in the dialog.");
            dialogueContent.CharacterName = EditorGUILayout.TextField(characterNameGUIContent, dialogueContent.CharacterName);
            
            GUIContent customizeCharacterNameGUIContent = new GUIContent("Customize Character Name Font", "Allows to modify the font of the character name for this dialogue.");
            if(dialogueContent.CharacterName != "")
            {
                dialogueContent.CustomizeCharacterName = EditorGUILayout.Toggle(customizeCharacterNameGUIContent, dialogueContent.CustomizeCharacterName);
            }
            else
            {
                EditorGUILayout.HelpBox("You can leave the character name field empty to not display the character name.", MessageType.Info);
            }
            
            GUIContent withLineBreakGUIContent = new GUIContent("With Line Break", "Add line break after character name.");
            GUIContent characterNameFontBoldGUIContent = new GUIContent("Bold", "Apply bold style to character name.");
            GUIContent characterNameFontItalicGUIContent = new GUIContent("Italic", "Apply italic style to character name.");
            GUIContent characterNameFontColorGUIContent = new GUIContent("Font Color", "The color of the font character name.");
            GUIContent characterNameFontSizeGUIContent = new GUIContent("Font Size", "The size of the font character name.");


            if(dialogueContent.CharacterName != "" && dialogueContent.CustomizeCharacterName)
            {
                dialogueContent.WithLineBreak = EditorGUILayout.Toggle(withLineBreakGUIContent, dialogueContent.WithLineBreak);
                dialogueContent.CharacterNameFontBold = EditorGUILayout.Toggle(characterNameFontBoldGUIContent, dialogueContent.CharacterNameFontBold);
                dialogueContent.CharacterNameFontItalic = EditorGUILayout.Toggle(characterNameFontItalicGUIContent, dialogueContent.CharacterNameFontItalic);
                dialogueContent.CharacterNameFontColor = EditorGUILayout.ColorField(characterNameFontColorGUIContent, dialogueContent.CharacterNameFontColor);
                if(dialogueContent.CharacterNameFontColor.a == 0) {
                    EditorGUILayout.HelpBox("Warning: Be careful the color is transparent. Modify the alpha.", MessageType.Warning);
                    Debug.LogWarning("Warning: Be careful, the Character Name Font Color of the DialogueContent script is transparent. Modify the alpha.");
                }
                dialogueContent.CharacterNameFontSize = EditorGUILayout.IntField(characterNameFontSizeGUIContent, dialogueContent.CharacterNameFontSize);
                EditorGUILayout.HelpBox("if Font Size is set to 0 it will have the same font size as the dialogue.", MessageType.Info);

            }

            GUIContent characterSpriteGUIContent = new GUIContent("Character Sprite", "Add character sprite in dialogue.");
            dialogueContent.CharacterImageSprite = (Sprite)EditorGUILayout.ObjectField(characterSpriteGUIContent, dialogueContent.CharacterImageSprite, typeof(Sprite), true);
            if(dialogueContent.CharacterImageSprite != null && dialogueContent.CharacterImageSprite.rect.width != dialogueContent.CharacterImageSprite.rect.height)
            {
                EditorGUILayout.HelpBox("It is recommended that the width and height of the sprite be the same size.", MessageType.Warning);
            }
            if(dialogueContent.CharacterImageSprite != null)
            {
                dialogueContent.CharacterImagePosition = (CharacterImagePosition)EditorGUILayout.EnumPopup("Character Image Position", (CharacterImagePosition)dialogueContent.CharacterImagePosition);
            }

            GUIContent nextDialogueGUIContent = new GUIContent("Next Dialogue Content", "The next dialogue. Having empty identifies as this dialogue as last so it will close the DialogueUI");
            dialogueContent.NextDialogueContent = (DialogueContent)EditorGUILayout.ObjectField(nextDialogueGUIContent, dialogueContent.NextDialogueContent, typeof(DialogueContent), true);

            if(dialogueContent.NextDialogueContent == dialogueContent)
            {
                EditorGUILayout.HelpBox("Warning: The Next Dialogue Content field cannot be itself. It will generate a dialog loop.", MessageType.Warning);
            }

            if(dialogueContent.NextDialogueContent == null && dialogueContent.DialogueOptions.Count == 0)
                EditorGUILayout.HelpBox("If you leave the Next Dialogue Content empty, at the end of the dialogue it identifies that it is the last and close the DialogueUICanvas.", MessageType.Info);
            
            if(dialogueContent.NextDialogueContent != null && dialogueContent.DialogueOptions.Count > 0)
                EditorGUILayout.HelpBox("This Dialogue Content is a dialog with options. So, when it goes to display the next dialogue, it won't switch to the dialogue of this NextDialogue field.", MessageType.Info);
            
            EditorGUILayout.LabelField("All methods are called when this dialogue start.");
            EditorGUILayout.PropertyField(_dialogueStartEvent);
            EditorGUILayout.LabelField("All methods are called when this dialogue end.");
            EditorGUILayout.PropertyField(_dialogueEndEvent);
            
            //EditorGUILayout.LabelField("Add to convert dialogue with options. When having an option, the next dialogue will be called from said option.", GUILayout.Height(60));
            EditorGUILayout.PropertyField(_dialogueOptions);
            EditorGUILayout.HelpBox("Add to convert dialogue with options. When having an option, the next dialogue will be called from said option.", MessageType.Info);
            serializedObject.ApplyModifiedProperties();

            //GUIContent dialogueOptionsContentContent = new GUIContent("Next DialogueContent", "The next dialogue. Having empty identifies as this dialogue as last so it will close the DialogueUI");
            //dialogueContent.DialogueOptions = (List<DialogueOption>)EditorGUILayout.ObjectField(dialogueOptionsContentContent, dialogueContent.DialogueOptions, typeof(List<DialogueOption>), true);

            //dialogueContent.Dialogue = EditorGUILayout.TextField("Dialogue", dialogueContent.Dialogue, GUILayout.Height(60));
        }
    }
}

#endif