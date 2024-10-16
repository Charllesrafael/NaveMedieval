using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss3Comportamento2 : Boss1Comportamento1
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

            if (bossMovimento.TargetLonge(this.transform.position) && (bossMovimento.GetIdPonto() == 3 || bossMovimento.GetIdPonto() == 0))
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
