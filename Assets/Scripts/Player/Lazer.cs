using UnityEngine;

namespace Nephenthesys
{
    public class Lazer : MonoBehaviour
    {
        [SerializeField] private Dangerous dangerous;
        //[SerializeField] private AudioPoolingPlay audioPoolingPlay;
        [SerializeField] ArmaLazer ArmaLazer;
        bool in_cooldown_idle;
        
        void Update()
        {
            if (!in_cooldown_idle)// && ArmaLazer != null)
            {
                in_cooldown_idle = true;
                if(ArmaLazer != null)
                    ArmaLazer.ShotAudioTrigger(1);
                else
                    AudioSystem.instance.ShotAudioManager(1, 3, true);
                Invoke("release_cooldown_idle", ArmaLazer != null ? ArmaLazer.Lazer_repeat_sfx_cooldown : 0.06f);
            }
        }
        void release_cooldown_idle() { in_cooldown_idle = false; }

        private void OnParticleCollision(GameObject other)
        {
            Vida vida = other.gameObject.GetComponentInChildren<Vida>();

            if (vida)
            {
                if(ArmaLazer != null)
                    ArmaLazer?.ShotAudioHitTrigger(1);
                else
                    AudioSystem.instance.ShotAudioManager(1, 3, false);
                vida.RecebeDano(dangerous.DanoRef ? dangerous.DanoRef.value : 1, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Vida vida = other.gameObject.GetComponent<Vida>();
            if (vida)
            {
                if(ArmaLazer != null)
                    ArmaLazer?.ShotAudioHitTrigger(1);
                else
                    AudioSystem.instance.ShotAudioManager(1, 3, false);
                vida.RecebeDano(dangerous.DanoRef ? dangerous.DanoRef.value : 1, false);
            }
        }
    }
}
