using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    [System.Serializable]
    public struct Pontos
    {
        public Transform[] pontos;
    }

    public class ArmaProjetil : ArmaBase
    {
        [SerializeField] internal Tiro tiro;

        [SerializeField] internal float delayTiro;
        internal float delayAtualTiro;

        [SerializeField] internal Pontos[] pontosLevel;


        public virtual void Awake()
        {
            delayAtualTiro = Time.time;
        }

        public override void Atirar()
        {
            if (Time.time > delayAtualTiro + delayTiro)
            {
                delayAtualTiro = Time.time;

                CriarTiro();

                if (LevelArma != null)
                    AudioSystem.instance?.AudioConfigSetTrigger(GetSoundName(), false);
                    // ControlerTiro?.ShotAudioTrigger(LevelArma.value);
            }
        }

        public virtual void CriarTiro()
        {   
            foreach (var item in pontosLevel[LevelArma != null ? LevelArma.value - 1 : 0].pontos)
            {
                ControllerPool.Create(tiro, item.position, item.rotation);
            }
        }
    }
}
