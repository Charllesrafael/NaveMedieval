using Doozy.Engine.Soundy;
using UnityEngine;

namespace Nephenthesys
{
    public class PlayAudio : MonoBehaviour
    {
        [SerializeField] private bool musica;
        [SerializeField] private bool playOnStart;
        [SerializeField] private SoundyData[] soundyDatas;

        private void Start()
        {
            if (playOnStart)
                Play();
        }

        public void Play(string texto)
        {
            AudioSystem.instance?.AudioConfigSetTrigger(texto, false);
        }

        public void Play()
        {
            AudioSystem.instance?.Play(soundyDatas, musica);
        }
    }
}
