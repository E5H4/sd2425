using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Bozo.ModularCharacters
{
    [CreateAssetMenu(fileName = "BSMC_CharacterObject", menuName = "BSMC_CharacterObject")]
    public class BSMC_CharacterObject : ScriptableObject
    {

        [SerializeField] List<OufitParam> outfits = new List<OufitParam>();
        public List<Color> SkinColor = new List<Color>();
        public List<Color> EyeColor = new List<Color>();
        public Texture skinAccessory;

        [Header("BodyShapes")]
        public float Gender;
        public float ChestSize;
        public float FaceShape;
        public float height;
        public float headSize;
        public float shoulderWidth;

        [Header("FaceShapes")]
        public float LashLength;
        public float BrowSize;
        public float EarTipLength;
        [Space]
        public Vector3 EyeSocketPosition;
        public float EyeSocketRotation;
        public Vector3 EyeSocketScale = Vector3.one;
        public float EyeUp;
        public float EyeDown;
        public float EyeSquare;
        [Space]
        public float NoseWidth;
        public float NoseUp;
        public float NoseDown;
        public float NoseBridgeAngle;
        [Space]
        public float MouthWide;
        public float MouthThin;
        [Space]
        public float pupilSize;
        public float irisSize;
        public float outerIrisColorSharpness;
        public float innerIrisColorShapness;
        public Vector2 innerIrisColorOffset;


        public void SaveCharacter(OutfitSystem outfitSystem)
        {

            var OutfitListRef = new List<Transform>(outfitSystem.Outfits.Values);
            var OutfitList = new List<GameObject>();

            var OutfitDictonary = new Dictionary<string, GameObject>();

            foreach (var item in Resources.LoadAll("", typeof(Outfit)))
            {
                OutfitDictonary.Add(item.name, item.GameObject());
            }

            for (int i = 0; i < OutfitListRef.Count; i++)
            {
                if (OutfitListRef[i] == null) continue;
                var outfitName = OutfitListRef[i].name.Replace("(Clone)", "");
                var PrefabRef = OutfitDictonary[outfitName];
                OutfitList.Add(PrefabRef);
            }

            outfits = new List<OufitParam>();

            foreach (var outfit in OutfitList)
            {
                if (outfit == null) continue;

                var outfitScript = outfit.GetComponent<Outfit>();
                var referenceOutfit = outfitSystem.GetOutfit(outfitScript.Type);

                var outfitParam = new OufitParam();
                outfitParam.outfit = outfit;
                outfitParam.type = outfitScript.Type;
                outfits.Add(outfitParam);

                if (referenceOutfit != null)
                {
                    var outfitRenderer = referenceOutfit.GetComponentInChildren<Renderer>(true);
                    Material outfitMaterial = null;


                    for (int i = 0; i < outfitRenderer.materials.Length; i++)
                    {
                        var sort = outfitRenderer.materials[i].name.Split("_");

                        if (sort[1] == "Outfit")
                        {
                            outfitMaterial = outfitRenderer.materials[i];
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (outfitMaterial == null) continue;


                    for (int i = 0; i < outfitParam.colors.Length; i++)
                    {
                        outfitParam.colors[i] = outfitMaterial.GetColor("_Color_" + (i + 1));
                    }
                }

            }

            Gender = outfitSystem.Gender;
            ChestSize = outfitSystem.ChestSize;
            FaceShape = outfitSystem.FaceShape;
            height = outfitSystem.height;
            headSize = outfitSystem.headSize;
            shoulderWidth = outfitSystem.shoulderWidth;

            LashLength = outfitSystem.LashLength;
            BrowSize = outfitSystem.BrowSize;
            EyeSocketPosition = outfitSystem.EyeSocketPosition;
            EyeSocketRotation = outfitSystem.EyeSocketRotation;
            EyeSocketScale = outfitSystem.EyeSocketScale;
            EarTipLength = outfitSystem.EarTipLength;
            EyeUp = outfitSystem.EyeUp;
            EyeDown = outfitSystem.EyeDown;
            EyeSquare = outfitSystem.EyeSquare;
            NoseBridgeAngle = outfitSystem.NoseBridgeAngle;
            NoseWidth = outfitSystem.NoseWidth;
            NoseUp = outfitSystem.NoseUp;
            NoseDown = outfitSystem.NoseDown;
            MouthThin = outfitSystem.MouthThin;
            MouthWide = outfitSystem.MouthWide;

            SkinColor = new List<Color>();
            var skinMaterial = outfitSystem.GetCharacterBody().material;
            SkinColor.Add(skinMaterial.GetColor("_SkinTone"));
            SkinColor.Add(skinMaterial.GetColor("_SkinUnderTone"));
            SkinColor.Add(skinMaterial.GetColor("_BrowColor"));
            SkinColor.Add(skinMaterial.GetColor("_LashesColor"));
            SkinColor.Add(skinMaterial.GetColor("_FuzzColor"));
            SkinColor.Add(skinMaterial.GetColor("_UnderwearBottomColor_Opacity"));
            SkinColor.Add(skinMaterial.GetColor("_UnderwearTopColor_Opacity"));
            SkinColor.Add(skinMaterial.GetColor("_Acc_Color_1"));
            SkinColor.Add(skinMaterial.GetColor("_Acc_Color_2"));
            SkinColor.Add(skinMaterial.GetColor("_Acc_Color_3"));
            skinAccessory = skinMaterial.GetTexture("_Accessory");

            EyeColor = new List<Color>();
            var eyeMaterial = outfitSystem.GetCharacterBody().materials[1];
            EyeColor.Add(eyeMaterial.GetColor("_InnerColor"));
            EyeColor.Add(eyeMaterial.GetColor("_OuterColor"));
            EyeColor.Add(eyeMaterial.GetColor("_PupilColor"));
            EyeColor.Add(eyeMaterial.GetColor("_ScleraColor"));

            pupilSize = eyeMaterial.GetFloat("_PupilSize");
            irisSize = eyeMaterial.GetFloat("_IrisSize");
            outerIrisColorSharpness = eyeMaterial.GetFloat("_OuterIrisColorSharpness");
            innerIrisColorShapness = eyeMaterial.GetFloat("_InnerIrisColorShapness");
            innerIrisColorOffset = eyeMaterial.GetVector("_InnerIrisColorOffset");
        }

        public void LoadCharacter(Transform parent)
        {
            var outfitSystem = parent.GetComponent<OutfitSystem>();
            outfitSystem.RemoveAllOutfits();

            foreach (var item in outfits)
            {
                if (item.outfit == null)
                {
                    outfitSystem.RemoveOutfit(item.type, true);
                    continue;
                }

                var outfit = Instantiate(item.outfit, parent);
                var renderers = outfit.GetComponentsInChildren<Renderer>(true);
                Material outfitMat = null;

                foreach (var renderer in renderers)
                {
                    for (int i = 0; i < renderer.materials.Length; i++)
                    {
                        var sort = renderer.materials[i].name.Split("_");

                        if (sort[1] == "Outfit")
                        {
                            outfitMat = renderer.materials[i];

                            for (int c = 0; c < 5; c++)
                            {
                                outfitMat.SetColor("_Color_" + (c + 1), item.colors[c]);
                            }
                        }
                    }
                }
            }

            outfitSystem.SetGender(Gender);
            outfitSystem.SetChest(ChestSize);
            outfitSystem.SetFace(FaceShape);
            outfitSystem.SetHeight(height);
            outfitSystem.SetHeadSize(headSize);
            outfitSystem.SetShoulderWidth(shoulderWidth);
            outfitSystem.SetBrowThickness(BrowSize);
            outfitSystem.SetLashLength(LashLength);
            outfitSystem.SetEyePositionHeight(EyeSocketPosition.y);
            outfitSystem.SetEyePositionWidth(EyeSocketPosition.x);
            outfitSystem.SetEyePositionDepth(EyeSocketPosition.z);
            outfitSystem.SetEyeScaleHeight(EyeSocketScale.y);
            outfitSystem.SetEyeScaleWidth(EyeSocketScale.x);
            outfitSystem.SetEyeScaleDepth(EyeSocketScale.z);
            outfitSystem.SetEyeTilt(EyeSocketRotation);
            outfitSystem.SetEarElf(EarTipLength);
            outfitSystem.SetEyeUp(EyeUp);
            outfitSystem.SetEyeDown(EyeDown);
            outfitSystem.SetEyeSquare(EyeSquare);
            outfitSystem.SetNoseBridge(NoseBridgeAngle);
            outfitSystem.SetNoseWidth(NoseWidth);
            outfitSystem.SetNoseUp(NoseUp);
            outfitSystem.SetNoseDown(NoseDown);
            outfitSystem.SetMouthThin(MouthThin);
            outfitSystem.SetMouthWide(MouthWide);

            var skinMaterial = outfitSystem.GetCharacterBody().material;
            skinMaterial.SetColor("_SkinTone", SkinColor[0]);
            skinMaterial.SetColor("_SkinUnderTone", SkinColor[1]);
            skinMaterial.SetColor("_BrowColor", SkinColor[2]);
            skinMaterial.SetColor("_LashesColor", SkinColor[3]);
            skinMaterial.SetColor("_FuzzColor", SkinColor[4]);
            skinMaterial.SetColor("_UnderwearBottomColor_Opacity", SkinColor[5]);
            skinMaterial.SetColor("_UnderwearTopColor_Opacity", SkinColor[6]);
            skinMaterial.SetColor("_Acc_Color_1", SkinColor[7]);
            skinMaterial.SetColor("_Acc_Color_2", SkinColor[8]);
            skinMaterial.SetColor("_Acc_Color_3", SkinColor[9]);
            skinMaterial.SetTexture("_Accessory", skinAccessory);
            outfitSystem.SetSkin(skinMaterial, true);

            var eyeMaterial = outfitSystem.GetCharacterBody().materials[1];
            eyeMaterial.SetColor("_InnerColor", EyeColor[0]);
            eyeMaterial.SetColor("_OuterColor", EyeColor[1]);
            eyeMaterial.SetColor("_PupilColor", EyeColor[2]);
            eyeMaterial.SetColor("_ScleraColor", EyeColor[3]);

            eyeMaterial.SetFloat("_PupilSize", pupilSize);
            eyeMaterial.SetFloat("_IrisSize", irisSize);
            eyeMaterial.SetFloat("_OuterIrisColorSharpness", outerIrisColorSharpness);
            eyeMaterial.SetFloat("_InnerIrisColorShapness", innerIrisColorShapness);
            eyeMaterial.SetVector("_InnerIrisColorOffset", innerIrisColorOffset);

            outfitSystem.SetEyes(eyeMaterial);

            outfitSystem.SetEyesTransforms();
        }

        public List<GameObject> GetOutfitsList()
        {
            var list = new List<GameObject>();
            foreach (var item in outfits)
            {
                list.Add(item.outfit);
            }

            return list;
        }

        public Dictionary<OutfitType, GameObject> GetOutfitsDictionary()
        {
            var list = new Dictionary<OutfitType,GameObject>();
            foreach (var item in outfits)
            {
                list.Add(item.type, item.outfit);
            }

            return list;
        }

        [System.Serializable]
        private class OufitParam
        {
            public OutfitType type;
            public GameObject outfit;
            public Color[] colors = new Color[5];
        }
    }
}



