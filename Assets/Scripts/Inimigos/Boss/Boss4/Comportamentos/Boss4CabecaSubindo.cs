using UnityEngine;

namespace Nephenthesys
{
    public class Boss4CabecaSubindo : MonoBehaviour
    {
        public Animator ani;
        public Animator aniPai;
        public GameObject fumaca;
        internal Boss4Comportamento2 comportamento;
        public Boos4CabecaVida boos4CabecaVida;
        public ArmaProjetil[] armas;
        internal bool pronto;

        public void OnFumaca()
        {
            fumaca?.SetActive(true);
        }

        public void OffFumaca()
        {
            fumaca?.SetActive(false);
        }

        public void Ativa()
        {
            ani?.SetInteger("state", 1);
            pronto = true;
        }

        public void Desable()
        {
            ani?.SetInteger("state", 0);
        }

        public void Fim()
        {
            comportamento?.Desativar();
            comportamento?.onFimComportamento();
            Destroy(this.gameObject);
        }

        public void Acabou()
        {
            aniPai.SetTrigger("fim");
        }
    }
}
