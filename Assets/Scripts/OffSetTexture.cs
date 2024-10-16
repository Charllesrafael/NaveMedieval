using UnityEngine;

namespace Nephenthesys
{
    public class OffSetTexture : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public Vector2 velXY;

        private void Update()
        {
            meshRenderer.sharedMaterial.mainTextureOffset += velXY * Time.deltaTime;
        }
    }
}
