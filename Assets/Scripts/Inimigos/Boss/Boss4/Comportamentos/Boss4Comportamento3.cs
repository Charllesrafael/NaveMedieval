using System.Collections;
using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Comportamento3 : Boss4Comportamento
    {
        public float delayAtivacaoLazers = 2f;
        private bool AtivouLazers;
        public ArmaProjetil[] armas;
        public GameObject[] lazersMira;
        public GameObject[] lazersGrande;

        public override void Ativar(Action onFimComportamento = null)
        {
            base.Ativar(onFimComportamento);
        }

        public override void Comportamento()
        {
            if (boss.morto)
                return;

            foreach (var arma in armas)
                arma?.Atirar();

            if (!AtivouLazers)
            {
                AtivouLazers = true;
                StartCoroutine(AtivarLazers());
            }

            quantidadeSalvaTiros++;
        }

        IEnumerator AtivarLazers()
        {
            foreach (var lazer in lazersMira)
                lazer?.SetActive(true);

            yield return new WaitForSeconds(delayAtivacaoLazers);

            foreach (var lazer in lazersGrande)
                lazer?.SetActive(true);
        }

        public override void Desativar()
        {
            foreach (var lazer in lazersMira)
                lazer?.SetActive(false);

            foreach (var lazer in lazersGrande)
                lazer?.SetActive(false);

            AtivouLazers = false;
            base.Desativar();
        }
    }
}
