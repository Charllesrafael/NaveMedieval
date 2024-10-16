using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Nephenthesys/WaveData", order = 0)]
    [System.Serializable]
    public class WaveData : ScriptableObject
    {
        public List<DadosInimigo> dadosInimigos;
    }
}
