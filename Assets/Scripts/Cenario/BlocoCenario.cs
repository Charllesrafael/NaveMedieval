using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class BlocoCenario : MonoBehaviour
    {
        public float Tempovida = 20f;
        public float multiplicador = 2;
        public Transform Final;

        private IEnumerator Start()
        {
            Tempovida = Tempovida / ControlerCenario.instance.moverCenario.multiplicador;
            yield return new WaitForSeconds(Tempovida);
            Morrer();
        }

        private void Morrer()
        {
            ControlerCenario.instance.CriarnovoBloco();
            ControllerPool.Discard(this.gameObject);
        }
    }
}
