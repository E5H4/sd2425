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
        // Existing properties
        private SerializedProperty _skipDialogueActionReference;
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

        // Navigation action properties
        private SerializedProperty _navigateUpActionReference;
        private SerializedProperty _navigateDownActionReference;
        private SerializedProperty _selectOptionActionReference;

        // New sprite fields for button appearance
        private SerializedProperty _defaultButtonSprite;
        private SerializedProperty _hoverButtonSprite;

        private void OnEnable()
        {
            _skipDialogueActionReference = serializedObject.FindProperty("skipDialogueActionReference");
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

            _navigateUpActionReference = serializedObject.FindProperty("navigateUpActionReference");
            _navigateDownActionReference = serializedObject.FindProperty("navigateDownActionReference");
            _selectOptionActionReference = serializedObject.FindProperty("selectOptionActionReference");

            // Find the new sprite fields (make sure the variable names match exactly)
            _defaultButtonSprite = serializedObject.FindProperty("defaultButtonSprite");
            _hoverButtonSprite = serializedObject.FindProperty("hoverButtonSprite");
        }

        public override void OnInspectorGUI()
        {
            DialogueUIController dialogueUIController = (DialogueUIController)target;

            GUIContent fontTypingSpeedGUIContent = new GUIContent("Font Typing Speed", "Typing speed for each of the letters of the dialogue.");
            dialogueUIController.FontTypingSpeed = EditorGUILayout.FloatField(fontTypingSpeedGUIContent, dialogueUIController.FontTypingSpeed);

            EditorGUILayout.PropertyField(_keyCodeSkipDialogue);
            EditorGUILayout.PropertyField(_skipDialogueActionReference);

            EditorGUILayout.LabelField("Navigation Actions", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_navigateUpActionReference, new GUIContent("Navigate Up Action"));
            EditorGUILayout.PropertyField(_navigateDownActionReference, new GUIContent("Navigate Down Action"));
            EditorGUILayout.PropertyField(_selectOptionActionReference, new GUIContent("Select Option Action"));

            // Draw the new sprite fields for button appearance
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button Sprites", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_defaultButtonSprite, new GUIContent("Default Button Sprite"));
            EditorGUILayout.PropertyField(_hoverButtonSprite, new GUIContent("Hover Button Sprite"));

            dialogueUIController.DialogueFont = (Font)EditorGUILayout.ObjectField("Dialogue Font", dialogueUIController.DialogueFont, typeof(Font), true);
            dialogueUIController.FontColor = EditorGUILayout.ColorField("Font Color", dialogueUIController.FontColor);
            if (dialogueUIController.FontColor.a == 0)
            {
                EditorGUILayout.HelpBox("Warning: Be careful the color is transparent. Modify the alpha.", MessageType.Warning);
                Debug.LogWarning("Warning: Be careful, the Font Color of the DialogueUIController script is transparent. Modify the alpha.");
            }

            EditorGUILayout.PropertyField(_dialogueContents);
            dialogueUIController.FirstDialogueContent = (DialogueContent)EditorGUILayout.ObjectField("First Dialogue Content", dialogueUIController.FirstDialogueContent, typeof(DialogueContent), true);
            if (dialogueUIController.FirstDialogueContent == null)
                EditorGUILayout.HelpBox("Error: The First Dialogue Content field cannot be empty. The Dialogue will not be displayed.", MessageType.Error);

            EditorGUILayout.PropertyField(_currentDialogueContent);

            EditorGUILayout.Space();
            if (GUILayout.Button("Create DialogueContent", GUILayout.Height(20)))
            {
                dialogueUIController.CreateDialogueContent();
            }
            EditorGUILayout.Space();

            GUIContent dialogueGUIContent = new GUIContent("UI Component References", "These fields are only displayed in Prefab Mode.");
            if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
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