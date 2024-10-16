using System.Collections;
using UnityEngine;

namespace Nephenthesys
{

    public class DroneAirGirando : MonoBehaviour
    {
        [SerializeField] private Rigidbody rig;
        [SerializeField] internal GameObject particulaMorte;
        [SerializeField] private float velocidade = 1;
        [SerializeField] private float tempoVida = 1;
        internal Vector3 direction;

        private void Start()
        {
            rig.velocity = direction * velocidade;

            if (tempoVida > 0)
                StartCoroutine(IMorrer());
        }

        private IEnumerator IMorrer()
        {
            yield return new WaitForSeconds(tempoVida);

            if (particulaMorte)
                ControllerPool.Create(particulaMorte, this.transform.position, Quaternion.identity);
            ControllerPool.Discard(this.gameObject);
        }

        private void FixedUpdate()
        {
            rig.velocity = LimitadorTela.instance.LimitarMovimentacao(this.transform, rig.velocity, true);
        }
    }
}
