using System.Collections.Generic;
using UnityEngine;

namespace RingStack.Scripts.LevelElements
{
    public class GhostRingController : MonoBehaviour
    {
        [SerializeField] private List<Material> Materials;
        [SerializeField] private MeshRenderer Mesh;


        // Private Functions
        private void SetMeshColor(Material m)
        {
            Mesh.material = m;
        }
        
        // Public Functions
        public void SetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case(ColorType.Blue):
                    SetMeshColor(Materials[0]);
                    break;
                case(ColorType.Green):
                    SetMeshColor(Materials[1]);
                    break;
                case(ColorType.Pink):
                    SetMeshColor(Materials[2]);
                    break;
                case(ColorType.Yellow):
                    SetMeshColor(Materials[3]);
                    break;
            }
        }

    }
}