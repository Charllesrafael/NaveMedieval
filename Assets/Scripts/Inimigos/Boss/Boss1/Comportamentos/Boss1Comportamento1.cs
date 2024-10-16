using System.Collections;
using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss1Comportamento1 : BossComportamento
    {
        public int quantidadeSalvaTirosMax = 3;
        public float quantidadeSalvaTiros = 0;
        public BossMovimento bossMovimento;
        public BossBase boss;

        public float tempoAtirandoParado = 2f;

        public float tempoAtirandoParadoAtual = 0;
        public WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        public ArmaBase[] armas1;


        private Coroutine comportamento;

        public override void Ativar(Action onFimComportamento = null)
        {
            comportamento = StartCoroutine(TempoComportamento(onFimComportamento));
        }

        public virtual IEnumerator TempoComportamento(Action onFimComportamento)
        {
            quantidadeSalvaTiros = 0;

            while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
            {
                yield return waitForEndOfFrame;
                Comportamento();
            }
            onFimComportamento?.Invoke();
        }

        public override void Desativar()
        {
            if (comportamento != null)
                StopCoroutine(comportamento);
        }

        public virtual void Comportamento()
        {
            if (boss.morto)
                return;

            if (bossMovimento.TargetLonge(this.transform.position))
            {
                armas1[0].Atirar();
            }
            else
            {
                if (tempoAtirandoParadoAtual < tempoAtirandoParado)
                {
                    armas1[1].Atirar();
                    boss.rig.isKinematic = true;
                    tempoAtirandoParadoAtual += Time.deltaTime;
                }
                else
                {
                    tempoAtirandoParadoAtual = 0;
                    quantidadeSalvaTiros++;
                    bossMovimento.ProximoPonto();
                    boss.rig.isKinematic = false;
                }
            }
        }
    }
}
