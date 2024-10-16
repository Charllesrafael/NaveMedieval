using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boos4CabecaVida : BossVida
    {
        public BossVida vida;
        
        public override void Start()
        {
        }

        public override void RecebeDano(int _dano, bool ehAreaLimite, bool byPass = false)
        {
            if (LimitadorTela.instance.ForaCamera(this.transform.position))// || vida.morreu)
                return;


            if (Time.time > delayDanoCurrent + vida.delayDano || byPass)
            {
                delayDanoCurrent = Time.time;
                feedBackDano?.Efeito();

                if (vida.vidaInicial == -1)
                    return;

                if (vida != null)
                {
                    if (vida.vidaAtual - _dano > 0)
                    {
                        vida.vidaAtual -= _dano;
                    }
                    else
                    {
                        vida.vidaAtual = 0;
                        vida.morreu = true;
                        vida.Morrer(ehAreaLimite);
                    }
                }
                BarraVidaBoss.instance?.SetProgress(vida.vidaAtual);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "TiroPlayer" && other.gameObject.tag != "AreaLimite")
                return;

            Dangerous dangerous = other.GetComponent<Dangerous>();

            bool ehAreaLimite = false;
            if (other.gameObject.tag == "AreaLimite") { ehAreaLimite = true; }

            if (dangerous)
            {
                RecebeDano(dangerous.DanoRef ? dangerous.DanoRef.value : 1, ehAreaLimite);
            }
        }
    }
}
