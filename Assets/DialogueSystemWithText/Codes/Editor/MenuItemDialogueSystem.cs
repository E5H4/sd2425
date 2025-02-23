using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;

namespace DialogueSystemWithText
{
    public class MenuItemDialogueSystem : MonoBehaviour
    {
        [MenuItem("GameObject/DialogueSystemWithText/DialogueUI", false, 10)]
        private static void CreateDialogueUI()
        {
            var gameObject = Resources.Load("DialogueUI") as GameObject;
            var instantiatedObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            instantiatedObject.name = "DialogueUI";

            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }
        }
    }
}

#endif