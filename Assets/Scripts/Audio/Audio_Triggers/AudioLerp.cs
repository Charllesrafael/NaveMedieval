using UnityEngine;
using System.Collections;

namespace Nephenthesys
{
    public class AudioLerp : MonoBehaviour
    {
        public AudioSource audioSource;
        public float targetVolume = 1;
        public float timeInSeconds = 1;

        [SerializeField] GameObject objectToBeActivatedAfterCompletion;
        public bool isToGetAS_Singleton;

        void Start()
        {
            /*if (AudioController.Instance)
            {
                if (isToGetAS_Singleton) audioSource = AudioController.Instance.AS;
                StartCoroutine(LerpAudio());
            }*/
        }

        IEnumerator LerpAudio()
        {
            float currentTime = 0;
            float startVolume = audioSource.volume;
            while (currentTime < timeInSeconds)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / timeInSeconds);
                yield return null;
            }
            if (objectToBeActivatedAfterCompletion != null)
            {
                objectToBeActivatedAfterCompletion.SetActive(true);
            }
        }
    }
}