using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bozo.ModularCharacters
{
    public class OutfitHeightChange : MonoBehaviour
    {
        [SerializeField] float HeightOffset;
        private void OnEnable()
        {
            var System = GetComponentInParent<OutfitSystem>();
            if (System == null) return;

            System.transform.localPosition = new Vector3 (System.transform.localPosition.x, System.transform.localPosition.y + HeightOffset, System.transform.localPosition.z);
        }

        private void OnDisable()
        {
            var System = GetComponentInParent<OutfitSystem>();
            if (System == null) return;

            System.transform.localPosition = new Vector3(System.transform.localPosition.x, System.transform.localPosition.y - HeightOffset, System.transform.localPosition.z);
        }
    }
}
