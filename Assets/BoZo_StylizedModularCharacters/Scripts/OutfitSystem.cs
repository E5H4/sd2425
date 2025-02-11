using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bozo.ModularCharacters
{
    public enum OutfitType { Face, Head, HairFront, HairBack, Top, Bottom, Feet, LowerFace, Hat, Overall, Gloves }
    public class OutfitSystem : MonoBehaviour
    {
        public BSMC_CharacterObject characterData;
        private BSMC_CharacterObject _characterData;
        SkinnedMeshRenderer CharacterBody;
        public Material CharacterMaterial;
        private Bounds CharacterRenderBounds;
        public Dictionary<OutfitType, Transform> Outfits = new Dictionary<OutfitType, Transform>();

        [Header("BodyShapes")]
        [Range(0, 100)] public float Gender;

        [Range(0, 100)] public float ChestSize;
        [Range(0, 100)] public float FaceShape;

        [Range(-1, 1)] public float height;
        [Range(-1, 1)] public float headSize;
        [Range(-1, 1)] public float shoulderWidth;

        [Header("FaceShapes")]
        [Range(0, 100)] public float LashLength;
        [Range(0, 100)] public float BrowSize;
        [Range(0, 100)] public float EarTipLength;
        [Space]
        public Vector3 EyeSocketPosition;
        public float EyeSocketRotation;
        public Vector3 EyeSocketScale = Vector3.one;
        [Range(0, 100)] public float EyeUp;
        [Range(0, 100)] public float EyeDown;
        [Range(0, 100)] public float EyeSquare;
        [Space]
        [Range(0, 100)] public float NoseWidth;
        [Range(0, 100)] public float NoseUp;
        [Range(0, 100)] public float NoseDown;
        [Range(0, 100)] public float NoseBridgeAngle;
        [Space]
        [Range(0, 100)] public float MouthWide;
        [Range(0, 100)] public float MouthThin;


        private Transform eyeSocket_L;
        private Transform eyeSocket_R;

        private Transform root;
        private Transform head;
        private Transform leftsShoulder;
        private Transform rightsShoulder;

        public UnityAction<Outfit> OnOutfitChanged;
        bool initalized;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                Invoke("LoadFromObject", 0f);
            }
        }

        private void Awake()
        {
            CharacterBody = GetComponentInChildren<SkinnedMeshRenderer>();
            CharacterRenderBounds = CharacterBody.localBounds;
            CharacterMaterial = CharacterBody.sharedMaterials[0];
            GetBoneTransform();
            SetBlendShapes();
            SetEyesTransforms();
            initalized = true;
        }

        private void Start()
        {
            LoadFromObject();
        }

        private void SetBlendShapes()
        {
            SetGender(Gender);
            SetChest(ChestSize);
            SetFace(FaceShape);
            SetLashLength(LashLength);
            SetBrowThickness(BrowSize);
            SetEarElf(EarTipLength);
            SetEyeDown(EyeDown);
            SetEyeUp(EyeUp);
            SetEyeSquare(EyeSquare);
            SetNoseWidth(NoseWidth);
            SetNoseUp(NoseUp);
            SetNoseDown(NoseDown);
            SetNoseBridge(NoseBridgeAngle);
            SetMouthWide(MouthWide);
            SetMouthThin(MouthThin);
        }

        private void GetBoneTransform()
        {
            foreach (var bone in CharacterBody.bones)
            {
                if (bone.name == "Root") { root = bone; continue; }
                if (bone.name == "Head") { head = bone; continue; }
                if (bone.name == "LeftShoulder") { leftsShoulder = bone; continue; }
                if (bone.name == "RightShoulder") { rightsShoulder = bone; continue; }
                if (bone.name == "SocketLeft") { eyeSocket_L = bone; continue; }
                if (bone.name == "SocketRight") { eyeSocket_R = bone; continue; }
            };
        }

        [ContextMenu("Load")]
        public void LoadFromObject()
        {
            if (characterData)
            {
                if(_characterData != characterData)
                {
                    characterData.LoadCharacter(transform);
                    _characterData = characterData;
                }
            }
            SetBlendShapes();
            SetEyesTransforms();
            ApplyBodyTransforms();
        }

        public void LoadFromObject(BSMC_CharacterObject saveData)
        {
            characterData = saveData;
            LoadFromObject();
        }

        [ContextMenu("Save")]
        public void SaveToObject()
        {
            if (!characterData)
            {
                Debug.LogWarning("Character Data Field is empty. Please provide a BSMC_CharacterObject to " + transform.name);
                return;
            }
            characterData.SaveCharacter(this);
        }

        public void RemoveOutfit(Outfit outfit, bool destory)
        {
            if (Outfits.TryGetValue(outfit.Type, out Transform currentOutfitInSlot))
            {
                if (destory == true && currentOutfitInSlot != null)
                {
                    Destroy(currentOutfitInSlot.gameObject);
                    Outfits[outfit.Type] = null;
                }
            }

            OnOutfitChanged?.Invoke(null);
        }

        public void RemoveOutfit(OutfitType type, bool destory)
        {
            if (Outfits.TryGetValue(type, out Transform currentOutfitInSlot))
            {
                if (destory == true && currentOutfitInSlot != null)
                {
                    Destroy(currentOutfitInSlot.gameObject);
                    Outfits[type] = null;
                }
            }

            OnOutfitChanged?.Invoke(null);
        }

        public void RemoveAllOutfits()
        {
            List<Transform> list = new List<Transform>(Outfits.Values);
            foreach (var item in list) { Destroy(item.gameObject); }
            Outfits.Clear();
            OnOutfitChanged?.Invoke(null);
        }

        public void AttachSkinnedOutfit(Outfit outfit)
        {

            if (!initalized) return;

            //check if an outfit is already in that slot and replace it
            if (Outfits.TryGetValue(outfit.Type, out Transform currentOutfitInSlot))
            {
                if (Outfits[outfit.Type])
                {
                    if (outfit.transform != Outfits[outfit.Type].transform)
                    {
                        Destroy(currentOutfitInSlot.gameObject);
                    }
                    else
                    {
                        OnOutfitChanged?.Invoke(outfit);
                        return;
                    }
                }
                Outfits[outfit.Type] = outfit.transform;
            }
            else
            {
                Outfits.Add(outfit.Type, outfit.transform);
            }

            //Assigning the skin material from the base character
            var renderer = outfit.GetComponentInChildren<SkinnedMeshRenderer>();
            if (renderer)
            {
                var materialsSort = renderer.sharedMaterial.name.Split("_");
                if (materialsSort[1] == "Skin")
                {
                    renderer.sharedMaterial = CharacterMaterial;
                }
            }

            //Merging outfit bones or attaching outfit to specified bone
            if (outfit.AttachPoint == "" && renderer)
            {
                if(outfit.Initalized == false)
                {
                    var bones = renderer.bones;
                    var oldBones = renderer.bones;
                    for (int i = 0; i < bones.Length; i++)
                    {
                        for (int u = 0; u < CharacterBody.bones.Length; u++)
                        {
                            if(bones[i].name == CharacterBody.bones[u].name)
                            {
                                bones[i] = CharacterBody.bones[u];
                            }
                        }
                    }
                    renderer.bones = bones;
                    renderer.rootBone = CharacterBody.rootBone;
                    foreach (var bone in oldBones) { Destroy(bone.gameObject); }
                    outfit.Initalized = true;
                }
            }
            else
            {
                Transform bone = null;
                foreach (var item in CharacterBody.bones)
                {
                    if (item.name == outfit.AttachPoint)
                    {
                        bone = item;
                        break;
                    }
                }
                outfit.transform.parent = bone.transform;
                outfit.transform.position = bone.position;
                outfit.transform.rotation = bone.rotation;
                outfit.transform.localScale = Vector3.one;
            }

            //Adjusting Mesh bounds so the meshes don't unexpectingly disappear.
            if (renderer)
            {
                Bounds outfitBounds = renderer.localBounds;
                CharacterRenderBounds.Encapsulate(outfitBounds);
                UpdateCharacterBounds();
            }

            SetGender(Gender);
            SetChest(ChestSize);
            OnOutfitChanged?.Invoke(outfit);

        }

        public void UpdateCharacterBounds()
        {
            CharacterBody.localBounds = CharacterRenderBounds;
            foreach (var item in Outfits.Values)
            {
                if (item == null) continue;
                var renderer = item.GetComponentInChildren<SkinnedMeshRenderer>();
                if (renderer != null) renderer.localBounds = CharacterRenderBounds;
            }
        }

        public void SetHeight(float value)
        {
            height = value;
            ApplyBodyTransforms();
        }

        public void SetHeadSize(float value)
        {
            headSize = value;
            ApplyBodyTransforms();
        }

        public void SetShoulderWidth(float value)
        {
            shoulderWidth = value;
            ApplyBodyTransforms();
        }

        public void SetGender(float value)
        {
            Gender = value;
            if (!CharacterBody) return;

            CharacterBody.SetBlendShapeWeight(0, Gender);
            foreach (var item in Outfits.Values)
            {
                if (item == null) continue;
                var outfit = item.GetComponentInChildren<SkinnedMeshRenderer>();
                if (outfit != null)
                {
                    if (outfit.sharedMesh.blendShapeCount >= 1)
                    {
                        outfit.SetBlendShapeWeight(0, Gender);
                    }
                }
            }
        }

        public void SetChest(float value)
        {
            ChestSize = value;
            if (!CharacterBody) return;

            CharacterBody.SetBlendShapeWeight(0, Gender);
            foreach (var item in Outfits.Values)
            {
                if (item == null) continue;
                var outfit = item.GetComponentInChildren<SkinnedMeshRenderer>();
                if (outfit != null)
                {
                    if (outfit.sharedMesh.blendShapeCount >= 2)
                    {
                        outfit.SetBlendShapeWeight(1, ChestSize);
                    }
                }
            }
        }

        public void SetSkin(Material value, bool isInstance = false)
        {
            if (!CharacterBody) return;

            CharacterBody.material = value;

            CharacterMaterial = value;

            foreach (var item in Outfits.Values)
            {
                if (item == null) continue;
                var renderer = item.GetComponentInChildren<SkinnedMeshRenderer>();
                if (renderer)
                {
                    var materialsSort = renderer.material.name.Split("_");
                    if (materialsSort[1] == "Skin")
                    {
                        renderer.material = CharacterMaterial;
                    }
                }
            }
        }

        public void SetEyes(Material value)
        {
            if (!CharacterBody) return;
            var mats = CharacterBody.sharedMaterials;
            mats[1] = value;
            CharacterBody.sharedMaterials = mats;
        }

        public void SetAccessories(Texture2D accessory)
        {
            CharacterBody.material.SetTexture("_Accessory", accessory);
            SetSkin(CharacterBody.material);
        }

        public void ApplyBodyTransforms()
        {
            var heightValue = height + 1;
            root.localScale = new Vector3(heightValue, heightValue, heightValue);
            var headSizeValue = 1 + headSize - height;
            head.localScale = new Vector3(headSizeValue, headSizeValue, headSizeValue);

            var shoulderWidthValue = shoulderWidth + 1;
            leftsShoulder.localScale = new Vector3(shoulderWidthValue, shoulderWidthValue, 1);
            rightsShoulder.localScale = new Vector3(shoulderWidthValue, shoulderWidthValue, 1);
        }

        public bool CheckIfOutfitExists(OutfitType type)
        {
            if (Outfits.TryGetValue(type, out Transform currentOutfitInSlot))
            {
                if (currentOutfitInSlot == null) { return false; }
                if (currentOutfitInSlot.gameObject.activeSelf == false) { return false; }
                else { return true; }
            }
            else
            {
                return false;
            }
        }

        #region Eye Transforms

        public void SetEyePositionHeight(float value)
        {
            EyeSocketPosition = new Vector3(EyeSocketPosition.x, value, EyeSocketPosition.z);
            SetEyesTransforms();
        }

        public void SetEyePositionDepth(float value)
        {
            EyeSocketPosition = new Vector3(EyeSocketPosition.x, EyeSocketPosition.y, value);
            SetEyesTransforms();
        }

        public void SetEyePositionWidth(float value)
        {
            EyeSocketPosition = new Vector3(value, EyeSocketPosition.y, EyeSocketPosition.z);
            SetEyesTransforms();
        }

        public void SetEyeScaleHeight(float value)
        {
            EyeSocketScale = new Vector3(EyeSocketScale.x, value, EyeSocketScale.z);
            SetEyesTransforms();
        }

        public void SetEyeScaleDepth(float value)
        {
            EyeSocketScale = new Vector3(EyeSocketScale.x, EyeSocketScale.y, value);
            SetEyesTransforms();
        }

        public void SetEyeScaleWidth(float value)
        {
            EyeSocketScale = new Vector3(value, EyeSocketScale.y, EyeSocketScale.z);
            SetEyesTransforms();
        }

        public void SetEyeTilt(float value)
        {
            EyeSocketRotation = value;
            SetEyesTransforms();
        }

        #endregion

        public void SetEyesTransforms()
        {
            if (CharacterBody == null) { return; }

            if (eyeSocket_L == null || eyeSocket_R == null)
            {
                foreach (var bone in CharacterBody.bones) { if (bone.name == "SocketLeft") { eyeSocket_L = bone; } };
                foreach (var bone in CharacterBody.bones) { if (bone.name == "SocketRight") { eyeSocket_R = bone; } };
            }

            if (eyeSocket_L == null || eyeSocket_R == null)
            {
                return;
            }

            eyeSocket_L.localPosition = new Vector3(EyeSocketPosition.x, EyeSocketPosition.y, EyeSocketPosition.z);
            eyeSocket_R.localPosition = new Vector3(EyeSocketPosition.x, EyeSocketPosition.y, -EyeSocketPosition.z);
            eyeSocket_L.localRotation  = Quaternion.Euler(new Vector3(0, 0, EyeSocketRotation));
            eyeSocket_R.localRotation = Quaternion.Euler(new Vector3(0, 0, EyeSocketRotation));
            eyeSocket_L.localScale = new Vector3(EyeSocketScale.x, EyeSocketScale.y, EyeSocketScale.z);
            eyeSocket_R.localScale = new Vector3(EyeSocketScale.x, EyeSocketScale.y, EyeSocketScale.z);
        }

        #region Eye Values
        public void SetPupilSize(float value)
        {
            CharacterBody.materials[1].SetFloat("_PupilSize", value);
        }

        public void SetIrisSize(float value)
        {
            CharacterBody.materials[1].SetFloat("_IrisSize", value);
        }

        public void SetIrisOuterSize(float value)
        {
            CharacterBody.materials[1].SetFloat("_OuterIrisColorSharpness", value);
        }

        public void SetIrisInnerSize(float value)
        {
            CharacterBody.materials[1].SetFloat("_InnerIrisColorShapness", value);
        }

        public void SetIrisOffsetWidth(float value)
        {
            var offset = CharacterBody.materials[1].GetVector("_InnerIrisColorOffset");
            offset.x = value;
            CharacterBody.materials[1].SetVector("_InnerIrisColorOffset", offset);
        }

        public void SetIrisOffsetHeight(float value)
        {
            var offset = CharacterBody.materials[1].GetVector("_InnerIrisColorOffset");
            offset.y = value;
            CharacterBody.materials[1].SetVector("_InnerIrisColorOffset", offset);
        }

        #endregion


        #region Blendshapes
        public void SetFace(float value)
        {
            if (!CharacterBody) return;
            FaceShape = value;
            CharacterBody.SetBlendShapeWeight(1, value);
        }

        public void SetLashLength(float value)
        {
            if (!CharacterBody) return;
            LashLength = value;
            CharacterBody.SetBlendShapeWeight(2, value);
        }

        public void SetBrowThickness(float value)
        {
            if (!CharacterBody) return;
            BrowSize = value;
            CharacterBody.SetBlendShapeWeight(36, value);
        }

        public void SetEarElf(float value)
        {
            if (!CharacterBody) return;
            EarTipLength = value;
            CharacterBody.SetBlendShapeWeight(46, value);
        }

        #region Eyes
        public void SetEyeDown(float value)
        {
            if (!CharacterBody) return;
            EyeDown = value;
            CharacterBody.SetBlendShapeWeight(43, value);
        }

        public void SetEyeUp(float value)
        {
            if (!CharacterBody) return;
            EyeUp = value;
            CharacterBody.SetBlendShapeWeight(44, value);
        }

        public void SetEyeSquare(float value)
        {
            if (!CharacterBody) return;
            EyeSquare = value;
            CharacterBody.SetBlendShapeWeight(45, value);
        }

        #endregion

        #region Nose
        public void SetNoseWidth(float value)
        {
            if (!CharacterBody) return;
            NoseWidth = value;
            CharacterBody.SetBlendShapeWeight(38, value);
        }

        public void SetNoseUp(float value)
        {
            if (!CharacterBody) return;
            NoseUp = value;
            CharacterBody.SetBlendShapeWeight(40, value);
        }

        public void SetNoseDown(float value)
        {
            if (!CharacterBody) return;
            NoseDown = value;
            CharacterBody.SetBlendShapeWeight(39, value);
        }

        public void SetNoseBridge(float value)
        {
            if (!CharacterBody) return;
            NoseBridgeAngle = value;
            CharacterBody.SetBlendShapeWeight(37, value);
        }

        #endregion

        #region Mouth
        public void SetMouthWide(float value)
        {
            if (!CharacterBody) return;
            MouthWide = value;
            CharacterBody.SetBlendShapeWeight(41, value);
        }

        public void SetMouthThin(float value)
        {
            if (!CharacterBody) return;
            MouthThin = value;
            CharacterBody.SetBlendShapeWeight(42, value);
        }

        #endregion

        public Transform GetOutfit(OutfitType outfitType)
        {
            if (Outfits.TryGetValue(outfitType, out Transform item))
            {
                return item;
            }

            return null;
        }

        public List<Transform> GetOutfits()
        {
            return new List<Transform>(Outfits.Values);
        }

        #endregion

        public SkinnedMeshRenderer GetCharacterBody() { return CharacterBody; }


    }
}
