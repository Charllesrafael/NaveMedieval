using UnityEngine;

[CreateAssetMenu(fileName = "BoolRef", menuName = "Custom/BoolRef", order = 0)]
public class BoolRef : ScriptableObject
{
    public bool value;

    public bool Value { get => value; set => this.value = value; }
}