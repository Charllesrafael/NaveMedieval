using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss3Comportamento1 : Boss1Comportamento1
    {
        [SerializeField] internal Transform[] pontosMovimento;
        private int idponto = 0;

        public override void Ativar(Action onFimComportamento = null)
        {
            bossMovimento.SetPoints(pontosMovimento);
            bossMovimento.SetIdPonto(idponto);
            tempoAtirandoParadoAtual = 0;
            base.Ativar(onFimComportamento);
        }

        public override IEnumerator TempoComportamento(Action onFimComportamento)
        {
            quantidadeSalvaTiros = 0;

            while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
            {
                yield return waitForEndOfFrame;
                Comportamento();
            }
            idponto = bossMovimento.idPonto;
            onFimComportamento?.Invoke();
        }

        public override void Comportamento()
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
