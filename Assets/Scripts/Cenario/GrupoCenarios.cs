using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "GrupoCenarios", menuName = "Nephenthesys/GrupoCenarios", order = 0)]
    public class GrupoCenarios : ScriptableObject
    {
        public bool UsouBlocoInicial = true;
        public BlocoCenario BlocosInicial;
        public BlocoCenario[] Blocos;
    }
}
