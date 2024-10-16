namespace Nephenthesys
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "DataBaseScore", menuName = "Nephenthesys/DataBaseScore", order = 0)]
    public class DataBaseScore : ScriptableObject
    {
        public int[] scores;
    }
}
