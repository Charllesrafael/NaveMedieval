using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss3Comportamento3 : Boss1Comportamento1
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
                quantidadeSalvaTiros++;
                idponto = bossMovimento.idPonto;
            }
        }
    }
}
