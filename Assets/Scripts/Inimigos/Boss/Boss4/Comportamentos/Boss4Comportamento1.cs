using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Comportamento1 : Boss4Comportamento
    {
        public ArmaProjetil[] armas;

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

            quantidadeSalvaTiros++;
        }
    }
}
