using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;

namespace DialogueSystemWithText
{
    [CustomEditor(typeof(DialogueUIController))]
    public class DialogueUIControllerEditor : Editor
    {

        private SerializedProperty _keyCodeSkipDialogue;
        private SerializedProperty _dialogueContents;
        private SerializedProperty _currentDialogueContent;
        private SerializedProperty _dialogueTextWithoutImage;
        private SerializedProperty _dialogueTextForLeftImage;
        private SerializedProperty _dialogueTextForRightImage;
        private SerializedProperty _characterLeftImage;
        private SerializedProperty _characterRightImage;
        private SerializedProperty _dialogueOptionsLayout;
        private SerializedProperty _dialogueOptionButton;

        private void OnEnable()
        {
            _keyCodeSkipDialogue = serializedObject.FindProperty("_keyCodeSkipDialogue");
            _dialogueContents = serializedObject.FindProperty("_dialogueContents");
            _currentDialogueContent = serializedObject.FindProperty("_currentDialogueContent");
            _dialogueTextWithoutImage = serializedObject.FindProperty("_dialogueTextWithoutImage");
            _dialogueTextForLeftImage = serializedObject.FindProperty("_dialogueTextForLeftImage");
            _dialogueTextForRightImage = serializedObject.FindProperty("_dialogueTextForRightImage");
            _characterLeftImage = serializedObject.FindProperty("_characterLeftImage");
            _characterRightImage = serializedObject.FindProperty("_characterRightImage");
            _dialogueOptionsLayout = serializedObject.FindProperty("_dialogueOptionsLayout");
            _dialogueOptionButton = serializedObject.FindProperty("_dialogueOptionButton");
        }

        public override void OnInspectorGUI() {

            DialogueUIController dialogueUIController = (DialogueUIController)target;

            GUIContent fontTypingSpeedGUIContent = new GUIContent("Font Typing Speed", "Typing speed for each of the letters of the dialogue.");
            dialogueUIController.FontTypingSpeed = EditorGUILayout.FloatField(fontTypingSpeedGUIContent, dialogueUIController.FontTypingSpeed);
            
            EditorGUILayout.PropertyField(_keyCodeSkipDialogue);

            dialogueUIController.DialogueFont = (Font)EditorGUILayout.ObjectField("Dialogue Font", dialogueUIController.DialogueFont, typeof(Font), true);
            dialogueUIController.FontColor = EditorGUILayout.ColorField("Font Color", dialogueUIController.FontColor);
            if(dialogueUIController.FontColor.a == 0) {
                EditorGUILayout.HelpBox("Warning: Be careful the color is transparent. Modify the alpha.", MessageType.Warning);
                Debug.LogWarning("Warning: Be careful, the Font Color of the DialogueUIController script is transparent. Modify the alpha.");
            }
            
            EditorGUILayout.PropertyField(_dialogueContents);

            dialogueUIController.FirstDialogueContent = (DialogueContent)EditorGUILayout.ObjectField("First Dialogue Content", dialogueUIController.FirstDialogueContent, typeof(DialogueContent), true);
            if(dialogueUIController.FirstDialogueContent == null)
                    EditorGUILayout.HelpBox("Error: The First Dialogue Content field cannot be empty. The Dialogue will not be displayed.", MessageType.Error);
            
            EditorGUILayout.PropertyField(_currentDialogueContent);
            
            EditorGUILayout.Space();
            if(GUILayout.Button("Create DialogueContent", GUILayout.Height(20)))
            {
                dialogueUIController.CreateDialogueContent();
            }
            EditorGUILayout.Space();

            GUIContent dialogueGUIContent = new GUIContent("UI Component References", "These fields are only displayed in Prefab Mode.");

            if(UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(dialogueGUIContent);
            }

            EditorGUILayout.PropertyField(_dialogueTextWithoutImage);
            EditorGUILayout.PropertyField(_dialogueTextForLeftImage);
            EditorGUILayout.PropertyField(_dialogueTextForRightImage);
            EditorGUILayout.PropertyField(_characterLeftImage);
            EditorGUILayout.PropertyField(_characterRightImage);
            EditorGUILayout.PropertyField(_dialogueOptionsLayout);
            EditorGUILayout.PropertyField(_dialogueOptionButton);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif