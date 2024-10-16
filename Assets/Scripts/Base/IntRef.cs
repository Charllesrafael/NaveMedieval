using UnityEngine;

[CreateAssetMenu(fileName = "IntRef", menuName = "Custom/IntRef", order = 0)]
public class IntRef : ScriptableObject
{
    public int value;

    public int Value { get => value; set => this.value = value; }
}
