using Bozo.ModularCharacters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bozo.ModularCharacters
{
    public class OutfitFollowBlendShapes : MonoBehaviour
    {
        SkinnedMeshRenderer mesh;
        SkinnedMeshRenderer character;

        [SerializeField] List<int> shapes = new List<int>();
        public void Start()
        {
            var System = GetComponentInParent<OutfitSystem>();
            mesh = GetComponentInChildren<SkinnedMeshRenderer>();
            if (System == null) return;

            character = System.GetCharacterBody();

            var characterShapeName = character.sharedMesh.GetBlendShapeName(0);
            var characterShapeTitle = characterShapeName.Split('.')[0];

            shapes.Clear();
            for (int i = 0; i < mesh.sharedMesh.blendShapeCount; i++)
            {
                var shapeName = mesh.sharedMesh.GetBlendShapeName(i).Split(".")[1];
                var shapeIndex = character.sharedMesh.GetBlendShapeIndex(characterShapeTitle + "." + shapeName);

                if (shapeIndex != -1)
                {
                    shapes.Add(shapeIndex);
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                mesh.SetBlendShapeWeight(i, character.GetBlendShapeWeight(shapes[i]));
            }
        }
    }
}
