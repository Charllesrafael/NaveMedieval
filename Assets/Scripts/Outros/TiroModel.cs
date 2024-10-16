using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "TiroModel", menuName = "Nephenthesys/TiroModel", order = 0)]
    public class TiroModel : ScriptableObject
    {
        public float Velocidade = 1;
        public float Tempovida = 5;
        public GameObject EfeitoInicial;
        public GameObject EfeitoMorte;
    }
}
