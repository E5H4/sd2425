using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

namespace DialogueSystemWithText
{
    public class PrefabModeOnlyField : UnityEngine.PropertyAttribute
    {
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PrefabModeOnlyField))]
    public class PrefabModeOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null ) ? 0 : EditorGUI.GetPropertyHeight(property, label);
        }
    
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
                return;
            EditorGUI.PropertyField(position, property, label );
        }
    }
    #endif
}