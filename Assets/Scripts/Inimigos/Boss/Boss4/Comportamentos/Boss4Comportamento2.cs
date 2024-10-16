using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Comportamento2 : Boss4Comportamento
    {
        public float delayArmas = 0.3f;
        public Boss4CabecaSubindo prefabReference;
        internal Boss4CabecaSubindo cabecaCurrent;
        public BossVida vida;
        internal Action onFimComportamento;

        public override void Ativar(Action _onFimComportamento = null)
        {
            onFimComportamento = _onFimComportamento;
            base.Ativar(_onFimComportamento);
        }


        public override IEnumerator TempoComportamento(Action onFimComportamento)
        {
            quantidadeSalvaTiros = 0;
            boss.SetState(state);
            yield return new WaitForSeconds(delayPreparacaoPosicao);

            ani.SetInteger("state", aniState);
            while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
            {
                yield return waitForSeconds;
                if (cabecaCurrent == null)
                {
                    cabecaCurrent = Instantiate(prefabReference, GameManager.GetPlayer().transform.position, Quaternion.identity);
                    cabecaCurrent.comportamento = this;
                    cabecaCurrent.boos4CabecaVida.vida = vida;
                     cabecaCurrent.boos4CabecaVida.boss = boss;
                     cabecaCurrent.boos4CabecaVida.bossExplosao = boss.bossExplosao;
                }

                if (cabecaCurrent.pronto)
                {
                    foreach (var item in cabecaCurrent.armas)
                    {
                        item?.Atirar();
                        yield return new WaitForSeconds(delayArmas);
                    }

                    quantidadeSalvaTiros++;
                }
            }
            cabecaCurrent?.Acabou();
            cabecaCurrent = null;
        }
    }
}
