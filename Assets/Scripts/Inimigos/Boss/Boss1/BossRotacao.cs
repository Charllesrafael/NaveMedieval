using UnityEngine;

namespace Nephenthesys
{
    public class BossRotacao : MonoBehaviour
    {
        [SerializeField] private BossMovimento bossMovimento;
        [SerializeField] private bool podeRotacionar;

        public GameObject target;
        public Transform rotacaoEsquerda;
        public Transform rotacaoDireta;
        public float timeLerpRatation = 1;
        private Vector3 direcao;
        private Quaternion rotacaoAlvo;

        private void Update()
        {
            if (podeRotacionar)
                Rotacao();
        }

        private void Rotacao()
        {
            if (bossMovimento.TargetLonge(this.transform.position))
            {
                direcao = bossMovimento.GetDirecao(this.transform.position);

                if (direcao.x > 0)
                {
                    rotacaoAlvo = rotacaoDireta.localRotation;
                }
                else
                {
                    rotacaoAlvo = rotacaoEsquerda.localRotation;
                }

                target.transform.localRotation = Quaternion.Lerp(target.transform.localRotation, rotacaoAlvo, timeLerpRatation * Time.deltaTime);
            }
            else
            {
                target.transform.localRotation = Quaternion.Lerp(target.transform.localRotation, Quaternion.Euler(Vector3.zero), timeLerpRatation * Time.deltaTime);
            }
        }
    }
}
