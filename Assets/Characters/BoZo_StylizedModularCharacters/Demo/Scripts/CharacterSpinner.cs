using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bozo.ModularCharacters
{
    public class CharacterSpinner : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        public float spinDir;
        public Transform character;
        private Animator anim;
        float dizzyTimer = 1;

        bool spinning;

        public void SetCharacter(Transform character)
        {
            this.character = character;
            anim = character.GetComponentInChildren<Animator>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            spinning = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            spinning = true;
        }

        private void Start()
        {
            SetCharacter(character);
        }

        private void Update()
        {


            if (spinning)
            {
                spinDir = -Input.GetAxis("Mouse X") * 5;
                dizzyTimer = 0.5f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                spinning = false;
            }
            character.Rotate(0, spinDir, 0);
            spinDir = Mathf.Lerp(spinDir, 0, Time.deltaTime);
            dizzyTimer -= Time.deltaTime;

            if (dizzyTimer <= 0)
            {
                if (spinDir >= 5 || spinDir <= -5)
                {
                    anim.SetBool("Dizzy", true);
                }
                else
                {
                    anim.SetBool("Dizzy", false);
                }
            }
        }
    }
}
