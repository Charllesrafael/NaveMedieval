using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "FloatRef", menuName = "Custom/FloatRef", order = 0)]
    public class FloatRef : ScriptableObject
    {
        public float value;

        public float Value { get => value; set => this.value = value; }
    }
}
