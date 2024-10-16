using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Nephenthesys
{

    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        public AudioHolder audio_scriptable;
        public AudioMixer AM;
        //public AudioSource AS;


        [Tooltip("Valor minimo = x, Valor maximo =: y")]
        public Vector2 limiteAudio;


        //[Header("Boss: 0, Menu: 1, GameMode: 2, GameOver: 3, GameWin: 4")]
        //public List<AudioClip> audios;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            AM.SetFloat("_BGS_Param", NormalizarValorAudio(audio_scriptable.volume_BGS));
            AM.SetFloat("_SFX_Param", NormalizarValorAudio(audio_scriptable.volume_SFX));
        }

        public float NormalizarValorAudio(float valor)
        {
            if (valor == 0)
                return -80;

            return Mathf.Lerp(limiteAudio.x, limiteAudio.y, valor);
        }


        public void change_Master(float value)
        {
            AM.SetFloat("_AudioBase_Param", value);
            //audio_scriptable.volume_Master = value;
        }
        public void change_BGS(float value)
        {
            AM.SetFloat("_BGS_Param", NormalizarValorAudio(value));
            audio_scriptable.volume_BGS = value;
        }
        public void change_SFX(float value)
        {
            AM.SetFloat("_SFX_Param", NormalizarValorAudio(value));
            audio_scriptable.volume_SFX = value;
        }




        public void Boss()
        {
            //AS.clip = audios[0];
            ///AM.SetFloat("_AudioBase_Param", -20f);

            //AS.Play();
        }

        public void Menu()
        {
            //AS.clip = audios[1];
            /// AM.SetFloat("_AudioBase_Param", 0f);

            //AS.Play();
        }

        public void GameMode()
        {
            //AS.clip = audios[2];
            ///AM.SetFloat("_AudioBase_Param", 0f);

            //AS.Play();
        }

        public void GameOver()
        {
            //AS.clip = audios[3];
            ///AM.SetFloat("_AudioBase_Param", -20f);

            //AS.Play();
        }

        public void GameWin()
        {
            //AS.clip = audios[4];
            ///AM.SetFloat("_AudioBase_Param", -20f);

            //AS.Play();
        }
    }
}
