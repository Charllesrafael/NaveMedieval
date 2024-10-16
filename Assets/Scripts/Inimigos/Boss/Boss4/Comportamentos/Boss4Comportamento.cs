using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Comportamento : BossComportamento
    {
        public int aniState;
        public Animator ani;
        public Boss4 boss;
        public int state = 0;
        public float tempoSalvaTiro = 1f;
        public int quantidadeSalvaTirosMax = 3;
        public float delayPreparacaoPosicao = 1f;
        internal float quantidadeSalvaTiros = 0;
        public WaitForSeconds waitForSeconds;
        private Coroutine comportamento;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(tempoSalvaTiro);
        }

        public override void Ativar(Action onFimComportamento = null)
        {
            comportamento = StartCoroutine(TempoComportamento(onFimComportamento));
        }

        public virtual IEnumerator TempoComportamento(Action onFimComportamento)
        {
            quantidadeSalvaTiros = 0;
            boss.SetState(state);
            yield return new WaitForSeconds(delayPreparacaoPosicao);

            ani.SetInteger("state", aniState);
            while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
            {
                yield return waitForSeconds;
                Comportamento();
            }
            Desativar();
            ani.SetInteger("state", 0);
            onFimComportamento?.Invoke();
        }

        public override void Desativar()
        {
            if (comportamento != null)
                StopCoroutine(comportamento);
        }

        public virtual void Comportamento()
        {
            quantidadeSalvaTiros++;
        }
    }
}
