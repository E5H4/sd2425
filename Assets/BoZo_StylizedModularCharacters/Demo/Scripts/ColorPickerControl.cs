using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bozo.ModularCharacters
{
    public class ColorPickerControl : MonoBehaviour
    {
        public float currentHue;
        public float currentSat;
        public float currentVal;
        public float currentColor;

        [SerializeField] private RawImage hueImage;
        [SerializeField] private RawImage satValImage;
        [SerializeField] private RawImage outputImage;

        [SerializeField] private Slider hueSlider;

        private Texture2D hueTexture;
        private Texture2D svTexture;
        private Texture2D outputTexture;

        public Renderer colorObject;
        public Material colorMaterial;
        public int MaterialSlot;

        [SerializeField] Text objectName;
        [SerializeField] Image[] Swatches;
        [SerializeField] int currentSwatch;

        private int ActiveReferneceSet;
        private List<string[]> ReferneceSet = new List<string[]>();
        [SerializeField] string[] OutfitReferneceIDs;
        [SerializeField] string[] SkinReferneceIDs;
        [SerializeField] string[] AccessoryReferneceIDs;
        [SerializeField] string[] EyesReferneceIDs;

        private void Start()
        {
            CreateHueImage();
            CreateSVImage();
            CreateOutputImage();
            UpdateOutputImage();

            ReferneceSet.Add(OutfitReferneceIDs);
            ReferneceSet.Add(SkinReferneceIDs);
            ReferneceSet.Add(AccessoryReferneceIDs);
            ReferneceSet.Add(EyesReferneceIDs);
        }

        private void CreateHueImage()
        {
            hueTexture = new Texture2D(1, 16);
            hueTexture.wrapMode = TextureWrapMode.Clamp;
            hueTexture.name = "HueTexture";

            for (int i = 0; i < hueTexture.height; i++)
            {
                hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1));
            }

            hueTexture.Apply();

            currentHue = 0;
            hueImage.texture = hueTexture;
        }

        private void CreateSVImage()
        {
            svTexture = new Texture2D(16, 16);
            svTexture.wrapMode = TextureWrapMode.Clamp;
            svTexture.name = "SVTexture";

            for (int y = 0; y < svTexture.height; y++)
            {
                for (int x = 0; x < svTexture.width; x++)
                {
                    svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x / svTexture.width, (float)y / svTexture.height));
                }
            }

            svTexture.Apply();

            currentSat = 0;
            currentVal = 0;
            satValImage.texture = svTexture;
        }

        private void CreateOutputImage()
        {
            outputTexture = new Texture2D(1, 16);
            outputTexture.wrapMode = TextureWrapMode.Clamp;
            outputTexture.name = "OutputTexture";

            Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

            for (int i = 0; i < hueTexture.height; i++)
            {
                outputTexture.SetPixel(0, 1, currentColor);
            }

            outputTexture.Apply();

            currentHue = 0;
            outputImage.texture = outputTexture;
        }

        private void UpdateOutputImage()
        {
            Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

            for (int i = 0; i < outputTexture.height; i++)
            {
                outputTexture.SetPixel(0, i, currentColor);
            }

            outputTexture.Apply();

            if (!colorObject) return;

            Swatches[currentSwatch].color = currentColor;

            var oldColor = colorObject.materials[MaterialSlot].GetColor(ReferneceSet[ActiveReferneceSet][currentSwatch]);
            currentColor.a = oldColor.a;
            colorObject.materials[MaterialSlot].SetColor(ReferneceSet[ActiveReferneceSet][currentSwatch], currentColor);

            colorObject.materials[MaterialSlot] = colorMaterial;
        }

        public void SetSV(float S, float V)
        {
            currentSat = S;
            currentVal = V;
            UpdateOutputImage();
        }

        public void SetHSV(float H, float S, float V)
        {
            currentHue = H;
            currentSat = S;
            currentVal = V;
            UpdateOutputImage();
        }

        public void UpdateSVImage()
        {
            currentHue = hueSlider.value;

            for (int y = 0; y < svTexture.height; y++)
            {
                for (int x = 0; x < svTexture.width; x++)
                {
                    svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x / svTexture.width, (float)y / svTexture.height));

                }
            }

            svTexture.Apply();
            UpdateOutputImage();
        }

        public void ChangeSwatch(int value)
        {
            currentSwatch = value;
            var swatchColor = Swatches[currentSwatch].color;
            Color.RGBToHSV(swatchColor, out float h, out float s, out float v);
            hueSlider.value = h;
            SetHSV(h, s, v);
            UpdateSVImage();
        }

        public void ChangeObject(Transform ob)
        {
            if (ob == null)
            {
                return;
            }
            var renderers = ob.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0) return;
            colorObject = renderers[0];
            if (colorObject == null) return;

            ActiveReferneceSet = 0;

            for (int i = 0; i < colorObject.materials.Length; i++)
            {
                var sort = colorObject.materials[i].name.Split("_");

                if (sort[1] == "Outfit")
                {
                    colorMaterial = colorObject.materials[i];
                    MaterialSlot = i;
                    for (int u = 1; u < renderers.Length; u++)
                    {
                        renderers[u].materials = colorObject.materials;
                    }
                    break;
                }
                else
                {
                    colorMaterial = null;
                }
            }

            if (colorMaterial == null)
            {
                colorObject = null;
                foreach (var item in Swatches) { item.color = new Color(0, 0, 0, 0); }
                return;
            }

            for (int i = 0; i < Swatches.Length; i++)
            {
                Swatches[i].color = colorMaterial.GetColor("_Color_" + (i + 1));

            }

            ChangeSwatch(0);

            objectName.text = colorObject.name.Replace("(Clone)", "");

        }

        public void SelectSkin(Transform transform)
        {
            var system = transform.GetComponent<OutfitSystem>();

            colorMaterial = system.CharacterMaterial;
            colorObject = system.GetCharacterBody();
            ActiveReferneceSet = 1;
            MaterialSlot = 0;

            for (int i = 0; i < Swatches.Length; i++)
            {
                if(ReferneceSet[ActiveReferneceSet][i] != "")
                {
                    Swatches[i].color = colorMaterial.GetColor(ReferneceSet[ActiveReferneceSet][i]);
                }
                else
                {
                    Swatches[i].color = new Color(0, 0, 0, 0);
                }
            }

            colorObject.materials[MaterialSlot].SetColor(ReferneceSet[ActiveReferneceSet][currentSwatch],
                colorObject.materials[MaterialSlot].GetColor(ReferneceSet[ActiveReferneceSet][currentSwatch]));
            system.SetSkin(colorObject.materials[MaterialSlot]);
        }

        public void SelectEyes(Transform transform)
        {
            var system = transform.GetComponent<OutfitSystem>();

            MaterialSlot = 1;
            colorMaterial = system.GetCharacterBody().materials[1];
            colorObject = system.GetCharacterBody();
            ActiveReferneceSet = 3;

            colorObject.materials[MaterialSlot].SetColor(ReferneceSet[ActiveReferneceSet][currentSwatch],
            colorObject.materials[MaterialSlot].GetColor(ReferneceSet[ActiveReferneceSet][currentSwatch]));
            system.SetEyes(colorObject.materials[MaterialSlot]);

            for (int i = 0; i < Swatches.Length; i++)
            {
                if (ReferneceSet[ActiveReferneceSet][i] != "")
                {
                    Swatches[i].color = colorMaterial.GetColor(ReferneceSet[ActiveReferneceSet][i]);
                }
                else
                {
                    Swatches[i].color = new Color(0, 0, 0, 0);
                }
            }


        }

        public void SelectAcc(Transform transform)
        {
            var system = transform.GetComponent<OutfitSystem>();

            colorMaterial = system.CharacterMaterial;
            colorObject = system.GetCharacterBody();
            ActiveReferneceSet = 2;
            MaterialSlot = 0;

            for (int i = 0; i < Swatches.Length; i++)
            {
                if (ReferneceSet[ActiveReferneceSet][i] != "")
                {
                    Swatches[i].color = colorMaterial.GetColor(ReferneceSet[ActiveReferneceSet][i]);
                }
                else
                {
                    Swatches[i].color = new Color(0, 0, 0, 0);
                }
            }

            colorObject.materials[MaterialSlot].SetColor(ReferneceSet[ActiveReferneceSet][currentSwatch],
                colorObject.materials[MaterialSlot].GetColor(ReferneceSet[ActiveReferneceSet][currentSwatch]));
            system.SetSkin(colorObject.materials[MaterialSlot]);
        }

        public void CopyColor(Renderer copyOutfit)
        {
            Material FromMaterial = colorMaterial;
            Material ToMaterial = null;

            for (int i = 0; i < copyOutfit.materials.Length; i++)
            {
                var sort = copyOutfit.materials[i].name.Split("_");

                if (sort[1] == "Outfit")
                {
                    ToMaterial = copyOutfit.materials[i];
                    MaterialSlot = i;
                }
                else
                {
                    return;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                ToMaterial.SetColor("_Color_" + (i + 1), FromMaterial.GetColor("_Color_" + (i + 1)));
            }
        }
    }
}
