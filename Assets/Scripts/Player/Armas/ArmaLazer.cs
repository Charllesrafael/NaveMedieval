using UnityEngine;

namespace Nephenthesys
{

    public class ArmaLazer : ArmaBase
    {
        private bool Atirando = false;
        private int LazerLevel = 1;
        public float Lazer_repeat_sfx_cooldown = 0.2f;
        [SerializeField] private GameObject[] Lazers;
        public override void Atirar()
        {
            if (Atirando)
                return;

            Atirando = true;
            ChangeLazer();
        }

        private void ChangeLazer()
        {
            for (int i = 0; i < Lazers.Length; i++)
            {
                Lazers[i].SetActive((LevelArma.value - 1) == i);
            }
        }

        private void Update()
        {
            if (LazerLevel != LevelArma.value && Time.timeScale > 0 && ControllerInputs.instance.atirar)
            {
                LazerLevel = LevelArma.value;
                ChangeLazer();
            }

            if ((Time.timeScale > 0 && !ControllerInputs.instance.atirar) || !GameManager.instance.Playing)
            {
                foreach (var item in Lazers)
                {
                    item.SetActive(false);
                }
                Atirando = false;
            }
        }

        public void ShotAudioHitTrigger(int idLazer) //triga audio lazer quando colide com inimigo
        {
            AudioSystem.instance.ShotAudioManager(idLazer, LazerLevel, false);
        }
        
        public void ShotAudioTrigger(int idLazer) //triga audio lazer idle / atirando
        {
            AudioSystem.instance.ShotAudioManager(idLazer, LazerLevel, true);
        }
    }
}
