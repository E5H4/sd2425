using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Bozo.ModularCharacters
{


    public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        [SerializeField] Image PickerImage;

        private RawImage SVImage;

        private ColorPickerControl CC;
        private RectTransform rect;
        private RectTransform pickerTransform;


        private void Awake()
        {
            SVImage = GetComponent<RawImage>();
            CC = FindFirstObjectByType<ColorPickerControl>();
            rect = GetComponent<RectTransform>();
            pickerTransform = PickerImage.GetComponent<RectTransform>();

            pickerTransform = PickerImage.GetComponent<RectTransform>();
            pickerTransform.position = new Vector2(-(rect.sizeDelta.x * 0.5f), -(rect.sizeDelta.y * 0.5f));
        }

        private void UpdateColor(PointerEventData eventData)
        {
            Vector3 pos = rect.InverseTransformPoint(eventData.position);

            float deltaX = rect.sizeDelta.x * 0.5f;
            float deltaY = rect.sizeDelta.y * 0.5f;

            if (pos.x < -deltaX)
            {
                pos.x = -deltaX;
            }
            if (pos.x > deltaX)
            {
                pos.x = deltaX;
            }
            if (pos.y < -deltaY)
            {
                pos.y = -deltaY;
            }
            if (pos.y > deltaY)
            {
                pos.y = deltaY;
            }

            float x = pos.x + deltaX;
            float y = pos.y + deltaY;

            float xNorm = x / rect.sizeDelta.x;
            float YNorm = y / rect.sizeDelta.y;

            pickerTransform.localPosition = pos;

            PickerImage.color = Color.HSVToRGB(0, 0, 1 - YNorm);

            CC.SetSV(xNorm, YNorm);

        }
        public void OnDrag(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }
    }
}
