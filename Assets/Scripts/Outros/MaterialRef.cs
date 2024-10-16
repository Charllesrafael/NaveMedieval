using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "MaterialRef", menuName = "Custom/MaterialRef", order = 0)]
    public class MaterialRef : ScriptableObject
    {
        public Material value;
    }
}
