using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class TentaculoBoss : MonoBehaviour
    {
        public GameObject tentaculo;
        public GameObject splashEscoder;
        public GameObject pontoSplahs;
        public float delayTentaculoAtivacao = 1;
        public float tempoVida = 3;
        public Collider colisorTentaculo;
        public Animator tentaculoAnimator;

        public GameObject audio_tentaculo;
        public SpriteRenderer target;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(delayTentaculoAtivacao);
            if (tempoVida > 0)
                tentaculoAnimator.enabled = true;
        }

        public void TentaculoAtivo()
        {
            colisorTentaculo.enabled = true;
            audio_tentaculo?.SetActive(true);
            if(target== true) target.enabled = false;
        }

        public void ComecoEscondeuTentaculo()
        {
            colisorTentaculo.enabled = false;
        }

        public void EscondeuTentaculo()
        {
            if (tempoVida > 0)
            {
                if (splashEscoder && pontoSplahs)
                    Instantiate(splashEscoder, pontoSplahs.transform.position, pontoSplahs.transform.rotation);
                Destroy(tentaculo.gameObject);
            }
        }
    }
}
