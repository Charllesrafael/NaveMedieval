using UnityEngine;

namespace Nephenthesys
{
    public class Player : MonoBehaviour
    {
        public Animator animator;
        public Rigidbody _rigidbody;
        public float velocidade;
        public float offsetScreen = 2f;

        internal Vector3 direcao;
        private Vector3 posicaoPlayerNaTela;

        public GameObject target;
        public Collider ColliderVida;
        public Transform from;
        public Transform to;

        private float oldRotacao = 0.5f;
        public float timeLerpRatation = 1;

        private bool comecouGame = false;


        void Update()
        {
            if ((GameManager.instance == null || !GameManager.instance.Playing && !comecouGame))
                return;

            if (!animator.enabled)
                animator.enabled = true;

            if (!GameManager.instance.GameOn && !comecouGame)
                return;

            if (!comecouGame)
                comecouGame = true;


            if (GameManager.instance.GameOn)
            {
                if (GameManager.instance.Playing)
                {
                    Inputs();
                }
                else
                {
                    direcao.x = 0;
                    direcao.y = 0;
                    direcao.z = 0;
                }
            }
            else
            {
                direcao.x = 0;
                direcao.z = 1 * velocidade;
            }
            Rotacao();
        }

        private void Inputs()
        {
            direcao.x = ControllerInputs.instance.movimento.x;
            direcao.z = ControllerInputs.instance.movimento.y;
            direcao.y = 0;
        }

        private void Rotacao()
        {
            oldRotacao = Mathf.Lerp(oldRotacao, (0.5f + direcao.x), timeLerpRatation * Time.deltaTime);
            target.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, oldRotacao);
        }

        private void LateUpdate()
        {
            if (GameManager.instance == null)
                return;
            Mover();
        }

        private void Mover()
        {
            if (GameManager.instance.GameOn)
            {
                // _rigidbody.velocity = direcao * velocidade;
                _rigidbody.velocity = LimitadorTela.instance.LimitarMovimentacao(this.transform, direcao * velocidade);
            }
            else
            {
                _rigidbody.velocity = direcao;
            }
        }

        private void LimitarMovimentacao()
        {
            posicaoPlayerNaTela = Camera.main.WorldToScreenPoint(this.transform.position);
            if (posicaoPlayerNaTela.x >= (Screen.width - offsetScreen) && direcao.x > 0)
            {
                direcao.x = 0;
            }

            if (posicaoPlayerNaTela.x <= (0 + offsetScreen) && direcao.x < 0)
            {
                direcao.x = 0;
            }

            if (posicaoPlayerNaTela.y <= (0 + offsetScreen) && direcao.z < 0)
            {
                direcao.z = 0;
            }

            if (posicaoPlayerNaTela.y >= (Screen.height - offsetScreen) && direcao.z > 0)
            {
                direcao.z = 0;
            }
        }
    }
}
