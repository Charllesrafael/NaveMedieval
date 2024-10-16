using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Nephenthesys
{

    public class AudioPooling : MonoBehaviour
    {

        [SerializeField] private AudioMixerGroup audioMixerGroupEffect;

        private static AudioPooling instance;
        private List<AudioSource> audioSources;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            audioSources = new List<AudioSource>();
        }


        public static void Play(AudioClip _audio, float _pitch = 1, float _volume = 1)
        {
            if (instance == null)
                return;

            Play(new AudioData(_audio, _pitch, _volume));
        }

        public static void Play(AudioData _audioData)
        {
            if (instance == null)
                return;

            AudioSource currentAudioSource = null;
            currentAudioSource = instance.GetAudioSource(currentAudioSource);
            instance.ConfigAudioSource(_audioData, currentAudioSource);
            currentAudioSource.Play();
        }

        private void ConfigAudioSource(AudioData _audioData, AudioSource currentAudioSource)
        {
            if (_audioData._audioClip == null)
                return;

            currentAudioSource.outputAudioMixerGroup = audioMixerGroupEffect;
            currentAudioSource.clip = _audioData._audioClip;
            if (!_audioData.pitchVariavel)
            {
                currentAudioSource.pitch = _audioData._pitch;
            }
            else
            {
                currentAudioSource.pitch = _audioData._pitch2;
            }
            currentAudioSource.volume = _audioData._volume;
        }

        private AudioSource GetAudioSource(AudioSource currentAudioSource)
        {
            foreach (var _audioSource in audioSources)
            {
                if (!_audioSource.isPlaying)
                {
                    currentAudioSource = _audioSource;
                    break;
                }
            }

            if (currentAudioSource == null)
            {
                currentAudioSource = gameObject.AddComponent<AudioSource>();
                audioSources.Add(currentAudioSource);
            }

            return currentAudioSource;
        }
    }



    [System.Serializable]
    public class AudioData
    {
        public AudioClip _audioClip;

        [Range(0f, 1f)]
        public float _volume = 1;

        public bool pitchVariavel;

        [Range(-3f, 3f)]
        public float _pitch = 1;
        public float _pitch2 => UnityEngine.Random.Range(pitch.x, pitch.y);
        public Vector2 pitch;

        public AudioData()
        {
            _audioClip = null;
            _volume = 1;
            pitch = Vector2.one;
            _pitch = 1;
        }

        public AudioData(AudioClip audioClip, float volume = 1, float pitch = 1)
        {
            _audioClip = audioClip;
            _volume = volume;
            this.pitch = new Vector2(pitch, pitch);
            _pitch = pitch;
        }
    }
}
