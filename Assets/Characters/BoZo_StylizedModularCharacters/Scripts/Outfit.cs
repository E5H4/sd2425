using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bozo.ModularCharacters
{
    public class Outfit : MonoBehaviour
    {
        public bool Initalized;
        private OutfitSystem system;
        [SerializeField] bool removeOutfit;
        [field: SerializeField] public OutfitType Type { get; private set; }
        public string AttachPoint;

        public GameObject PrimaryVisual;
        [SerializeField] VisualSets[] AlternateVisual;
        [field: SerializeField] public IncompatibleSets[] IncompatibleSets { get; private set; }


        private void OnEnable()
        {
            system = GetComponentInParent<OutfitSystem>();
            if (system == null) return;

            if (transform.childCount != 0)
            {
                PrimaryVisual = transform.GetChild(0).gameObject;
            }

            system.OnOutfitChanged += CheckAltVisuals;
            if (removeOutfit == true) { system.RemoveOutfit(this, true); return; }
            else { system.AttachSkinnedOutfit(this); }

            CheckIncompatable();
            CheckAltVisuals(null);
        }

        private void OnDisable()
        {
            if (!system) return;
            system.OnOutfitChanged -= CheckAltVisuals;
            ReleaseIncompatable();
            system.RemoveOutfit(this, false);
        }

        private void OnDestroy()
        {
            if (!system) return;
            system.OnOutfitChanged -= CheckAltVisuals;
        }

        private void Start()
        {
            if (!system) return;
            CheckAltVisuals(null);

            if (removeOutfit) {system.RemoveOutfit(this, true); Destroy(gameObject); }
            else { system.AttachSkinnedOutfit(this); }
        }

        private void CheckAltVisuals(Outfit outfit)
        {
            if (gameObject == null) return;
            foreach (var alt in AlternateVisual)
            {
                if (system.CheckIfOutfitExists(alt.type))
                {
                    PrimaryVisual.SetActive(false);
                    if(alt.alternateVisual!= null)
                        alt.alternateVisual.SetActive(true);
                }
                else
                {
                    PrimaryVisual.SetActive(true);
                    if (alt.alternateVisual != null)
                        alt.alternateVisual.SetActive(false);
                }
            }
        }

        private void CheckIncompatable()
        {
            foreach(var set in IncompatibleSets)
            {
                var outfit = system.GetOutfit(set.type);
                if(outfit != null)
                {
                    if (set.destroyIncompatible)
                    {
                        Destroy(outfit.gameObject);
                    }
                    else
                    {
                        outfit.gameObject.SetActive(false);
                        set.AddHidden(outfit.gameObject);
                    }
                }
            }
        }

        private void ReleaseIncompatable()
        {
            foreach (var set in IncompatibleSets)
            {
                set.ReleaseHidden();
            }
        }
    }

    [System.Serializable]
    public class VisualSets
    {
        public OutfitType type;
        public GameObject alternateVisual;
    }
    [System.Serializable]
    public class IncompatibleSets
    {
        public OutfitType type;
        public bool destroyIncompatible;
        public List<GameObject> HiddenObjects = new List<GameObject>();

        public void AddHidden(GameObject outfit)
        {
            HiddenObjects.Add(outfit);
        }

        public void ReleaseHidden()
        {
            foreach (var item in HiddenObjects)
            {
                if (item)
                item.gameObject.SetActive(true);
            }
            HiddenObjects.Clear();
        }
    }

}
