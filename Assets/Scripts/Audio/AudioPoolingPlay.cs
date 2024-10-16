using UnityEngine;

namespace Nephenthesys
{
    public class AudioPoolingPlay : MonoBehaviour
    {
        [SerializeField] private bool playOnStart = false;
        [SerializeField] AudioData audioData;

        private void Start()
        {
            if (playOnStart)
                AudioPooling.Play(audioData);

        }

        public void Play()
        {
            AudioPooling.Play(audioData);
        }
    }
}
