using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class ItemUpgrade : MonoBehaviour
    {
        [SerializeField] private Rigidbody rig;
        [SerializeField] private int IdScore;
        [SerializeField] internal GameObject particulaColeta;
        [SerializeField] internal GameObject particulaMorte;
        [SerializeField] private float tempoVida = 1;
        [SerializeField] private float velocidade = 1;
        private Vector3 direction;
        private bool _coletado = false;

        private void Start()
        {
            float valor = Random.Range(0f, 360f);
            direction.x = Mathf.Cos(valor);
            direction.z = Mathf.Sin(valor);
            direction.y = 0f;
            rig.velocity = direction * velocidade;

            if (tempoVida > 0)
                StartCoroutine(IMorrer());
        }

        private void FixedUpdate()
        {
            rig.velocity = LimitadorTela.instance.LimitarMovimentacao(this.transform, rig.velocity, true);
        }

        private void OnTriggerEnter(Collider other)
        {
            Coletado(other);
        }

        public virtual void Coletado(Collider other)
        {
            if (_coletado)
                return;

            PlayerVida playerVida = other.GetComponent<PlayerVida>();
            if (playerVida)
            {
                if (_coletado)
                    return;
                _coletado = true;

                AcaoItem(playerVida);

                if (particulaColeta)
                    ControllerPool.Create(particulaColeta, this.transform.position, Quaternion.identity);
                ControllerPool.Discard(this.gameObject);
            }
            else
            {
                ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
                if (particleSystem)
                    ControllerPool.Discard(this.gameObject);
            }

        }

        public virtual void AcaoItem(PlayerVida playerVida)
        {
            ScoreUI.CriarTextUIScore(IdScore, this.transform.position);
            playerVida.RecebeVida(1);
        }

        private IEnumerator IMorrer()
        {
            yield return new WaitForSeconds(tempoVida);

            if (particulaMorte)
                ControllerPool.Create(particulaMorte, this.transform.position, Quaternion.identity);
            ControllerPool.Discard(this.gameObject);
        }
    }
}
