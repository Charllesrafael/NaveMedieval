using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class AtirandoDelay : MonoBehaviour
    {
        [SerializeField] private Tiro prefabTiro;
        [SerializeField] private Transform[] pontosTiro;
        [SerializeField] private float DelayComecaAtirar;
        [SerializeField] private float DelayTiroRepetitivo;

        public bool isTiroAzul = true;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(DelayComecaAtirar);
            do
            {
                Atirar();
                yield return new WaitForSeconds(DelayComecaAtirar);
            } while (DelayTiroRepetitivo > 0);
        }

        private void Atirar()
        {
            foreach (var ponto in pontosTiro)
            {
                ControllerPool.Create(prefabTiro, ponto.position, ponto.rotation);
                // ControllerPool.CreateTiro(prefabTiro, ponto.position, ponto.rotation);
            }
            if (isTiroAzul) { AudioSystem.instance?.AudioConfigSetTrigger("Enemy_Shot_1", false); }
            else { AudioSystem.instance?.AudioConfigSetTrigger("Enemy_Shot_2", false); }
        }
    }
}