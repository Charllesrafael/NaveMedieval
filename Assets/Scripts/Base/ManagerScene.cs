using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Audio;

namespace Nephenthesys
{
    public class ManagerScene : MonoBehaviour
    {
        public static ManagerScene instance;
        [SerializeField] private MMFeedbacks UnPauseTime;
        [SerializeField] internal int idFaseAtual = 0;
        [SerializeField] internal int continues = 0;
        [SerializeField] internal int bonusNoDamage = 1000;
        [SerializeField] internal int bonusNoDrone = 1000;
        [SerializeField] internal int naveSelecionada;
        [SerializeField] internal bool pulaHistoria;

        [SerializeField] internal IntRef scoreValue;

        internal Action IniciarHistoria;

        public string scenaHistoriaInicial;
        public string scenaHistoriaFinal;
        public float time2wait4loadingNewAudio = 3.6f;
        public TotemData totemData;
        public string[] scenas;

        public float transicaoSnapshot = 0.2f;
        public AudioMixerSnapshot loadingSnapshot;
        public AudioMixerSnapshot NOTloadingSnapshot;


        void Awake()
        {
            if (instance != null)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                idFaseAtual = 0;
                naveSelecionada = 0;
            }
        }

        public void OnIniciarHistoria()
        {
            IniciarHistoria?.Invoke();
        }

        public void SelecaoNave()
        {
            pulaHistoria = false;
            LoadingManager.LoadSceneSingle("SelectAsset");
            naveSelecionada = 0;
            //TranslatetoSnapshot_Normal();
        }

        internal void Tutorial()
        {
            LoadingManager.LoadSceneSingle("Tutorial");
            // TranslatetoSnapshot_Normal();
        }

        public void ProximaFase(bool _pulaHistoria = false)
        {
            if (idFaseAtual < scenas.Length)
                idFaseAtual++;

            if (idFaseAtual >= scenas.Length)
            {
                LoadingManager.LoadSceneSingle(scenaHistoriaFinal);
                return;
            }
            ReiniciarFase(_pulaHistoria);
        }

        public void FaseAnterior(bool _pulaHistoria = false)
        {
            if (idFaseAtual > 0)
                idFaseAtual--;

            ReiniciarFase(_pulaHistoria);
        }

        internal static void ComecarHistoria()
        {
            LoadingManager.LoadSceneSingle(instance.scenaHistoriaInicial);
        }

        internal void ComecarJogo(bool evitaAudioMenu)
        {
            idFaseAtual = 0;
            continues = 3;
            scoreValue.value = 0;
            ReiniciarFase();
            // Menu(evitaAudioMenu);
        }

        public void ReiniciarFase(bool _pulaHistoria = false)
        {
            pulaHistoria = _pulaHistoria;
            UnPauseTime?.PlayFeedbacks();

            LoadingManager.LoadSceneAdditive(instance.scenas[idFaseAtual], "GameUI");
            //GameManager.instance?.TranslatetoSnapshot_Normal();
        }

        public void Menu(bool evitaAudioMenu)
        {
            pulaHistoria = false;
            LoadingManager.LoadSceneSingle("Menu");
            if (!evitaAudioMenu) AudioSystem.instance.AudioConfigSetTrigger("Menu", true);
            //TranslatetoSnapshot_Normal();
        }

        public void reset_audio_parameters_when_coming_from_menu()
        {
            AudioSystem.instance.isPlayingBossBgm = false; AudioSystem.instance.setRepeatBGS(false);////<<<<
        }

        internal static bool HasStory()
        {
            if (instance == null || instance.IniciarHistoria == null || ManagerScene.instance.IniciarHistoria.GetInvocationList().Length == 0)
                return false;

            return true;
        }

        public void TranslatetoSnapshot_minimized()
        {
            loadingSnapshot.TransitionTo(transicaoSnapshot);
        }

        public void TranslatetoSnapshot_Normal()
        {
            NOTloadingSnapshot.TransitionTo(transicaoSnapshot);
        }

        public int getIdFase()
        {
            return idFaseAtual;
        }

        public int GetAtualArmaId()
        {
            return totemData.GetArmaIdNave(naveSelecionada);
        }

        internal GameObject GetNavePrefab(int _naveSelecionada)
        {
            return totemData.GetNavePrefab(_naveSelecionada);
        }

        internal NaveData GetNave(int _naveSelecionada)
        {
            return totemData.GetNave(_naveSelecionada);
        }

        internal NaveData GetNaveAtual()
        {
            return totemData.GetNave(naveSelecionada);
        }

        internal List<NaveData> GetAllNavesPrefabs()
        {
            return totemData.GetAllNaves();
        }

        internal int GetMaxNaves()
        {
            return totemData.GetMaxNaves();
        }

    #if UNITY_EDITOR
        [MenuItem("Save/ResetAllSave", false, 0)]
        public static void ResetAllSave()
        {
            PlayerPrefs.DeleteAll();
        }
    #endif
    }
}
