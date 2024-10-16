using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ArmaProjetilMissel : ArmaProjetil
    {
        public TiroMisselPlayer _tiro;
        [SerializeField] internal DetertorInimigos Detector;
        [SerializeField] internal float quantidadeTiros;
        internal float quantidadeAtualTiros;
        [SerializeField] internal float delayEntreTiro;
        internal float delayEntreAtualTiro;
        internal DetertorInimigos DetectorAtual;
        public Action OnFire;

        private void Start() {
            DetectorAtual = ControllerPool.Create(Detector, this.transform.position, Quaternion.identity);
            DetectorAtual.targetSeguir =  this;
            quantidadeAtualTiros = 0;
            OnFire += Atirando;
        }

        public override void Atirar()
        {
            if(quantidadeAtualTiros < quantidadeTiros)
            {
                if (Time.time > delayEntreAtualTiro + delayEntreTiro)
                {
                    delayEntreAtualTiro = Time.time;
                    OnFire?.Invoke();
                    quantidadeAtualTiros++;

                    if (LevelArma != null)
                        AudioSystem.instance?.AudioConfigSetTrigger(GetSoundName(), false);
                        // ControlerTiro?.ShotAudioTrigger(LevelArma.value);
                }
            }else
            {
                if (Time.time > delayAtualTiro + delayTiro)
                {
                    delayAtualTiro = Time.time;
                    quantidadeAtualTiros = 0;
                }
            }
        }

        private void Atirando()
        {
            foreach (var item in pontosLevel[LevelArma != null ? LevelArma.value - 1 : 0].pontos)
            {
                TiroMisselPlayer tiroMissel = ControllerPool.Create(_tiro, item.position, item.rotation);
                tiroMissel.armaProjetilMissel = this;
            }
        }
    }
}
