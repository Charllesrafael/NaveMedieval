using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss3Comportamento4 : Boss1Comportamento1
    {
        [SerializeField] internal Transform[] pontosMovimento;
        private int idponto = 0;
        private bool ComecaoComportameto = false;

        public override void Ativar(Action onFimComportamento = null)
        {
            bossMovimento.SetPoints(pontosMovimento);
            bossMovimento.SetIdPonto(idponto);
            bossMovimento.loopMovimento = true;
            tempoAtirandoParadoAtual = 0;
            ComecaoComportameto = true;
            base.Ativar(onFimComportamento);
        }

        public override void Comportamento()
        {
            if (boss.morto)
                return;

            if (bossMovimento.TargetLonge(this.transform.position))
            {
                if (ComecaoComportameto)
                    armas1[0].Atirar();
            }
            else
            {
                tempoAtirandoParadoAtual = 0;
                quantidadeSalvaTiros++;
                ComecaoComportameto = false;
                bossMovimento.ProximoPonto();
            }
        }

        public override IEnumerator TempoComportamento(Action onFimComportamento)
        {
            quantidadeSalvaTiros = 0;

            while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
            {
                yield return waitForEndOfFrame;
                Comportamento();
            }
            bossMovimento.loopMovimento = false;
            onFimComportamento?.Invoke();
        }
    }
}
