using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Nephenthesys
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public float delayVitoria = 1f;
        public float delayDerrota = 1f;
        [SerializeField] private float tempoPlayerIndoEmbora = 1;
        public bool _playing = false;
        public bool gameOn = false;
        [SerializeField] private GameObject painelVitoria;
        [SerializeField] private GameObject painelDerrota;
        [SerializeField] private PlayerVida player;
        internal bool NoDrone = true;
        internal bool NoDamage = true;
        internal bool temDrone = false;
        internal bool playerInvencivel = false;
        [SerializeField] internal GameObject upgrade;
        [SerializeField] internal GameObject downgrade;

        [Range(0, 100)]
        [SerializeField] internal int chanceDrop = 0;
        [Range(0, 100)]
        [SerializeField] internal int chanceDropUpgrade = 0;

        internal int scoreAnterior;


        public PlayerVida Player
        {
            get
            {
                if (player == null)
                    player = GameObject.FindObjectOfType<PlayerVida>();

                return player;
            }
            set => player = value;
        }

        public bool Playing { get => _playing; set => _playing = value; }
        public bool GameOn { get => gameOn; set => gameOn = value; }

        [SerializeField] internal MMFeedbacks feedbackVitoria;
        [SerializeField] internal MMFeedbacks feedbackMorte;

        private void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
            scoreAnterior =  ManagerScene.instance.scoreValue.value;
        }

        public static void SetPlayer(PlayerVida player)
        {
            if (instance == null || !instance.Playing)
                return;
            instance.Player = player;
        }

        public static PlayerVida GetPlayer()
        {
            if (instance == null)
            {
                Debug.LogWarning("instance = null em GameManager");
                return null;
            }
            return instance.Player;
        }

        public static int GetVidaInicial()
        {
            PlayerVida playerVida = GetPlayer();
            if (playerVida != null)
                return playerVida.GetVidaInicial();


            Debug.LogWarning("playerVida = null em GameManager");
            return 1;
        }

        public static void GanhouOJogo()
        {
            if (instance == null)
                return;

            instance.StartCoroutine(Esperar(instance.delayVitoria, () =>
            {
                instance.StartCoroutine(instance.PlayerIndoEmbora());
            }));
        }

        public static void BlockPlayerController()
        {
            if (instance == null)
                return;
            instance.Playing = false;
            instance.Player.ColliderVida.enabled = false;
        }

        IEnumerator PlayerIndoEmbora()
        {
            gameOn = false;
            yield return new WaitForSeconds(instance.tempoPlayerIndoEmbora);
            instance.feedbackVitoria?.PlayFeedbacks();
            AudioSystem.instance.AudioConfigSetTrigger("Win", false);
            ManagerScene.instance.TranslatetoSnapshot_minimized();

            instance.painelVitoria.SetActive(true);
            instance.Playing = false;
        }

        public void PerdeuOJogo()
        {
            if (instance == null || !instance.Playing)
                return;

            instance.Playing = false;

            //AudioSystem.instance.setRepeatBGS(true);//<<<<<<<<<<<<<
            if (AudioSystem.instance != null)
            {
                if (!AudioSystem.instance.isPlayingBossBgm)
                { AudioSystem.instance.setRepeatBGS(true); }//<<<<<<<<<<<<<
                else { AudioSystem.instance.setRepeatBGS(false); }
            }

            instance.StartCoroutine(Esperar(instance.delayDerrota, () =>
            {
                instance.feedbackMorte?.PlayFeedbacks();
                ManagerScene.instance?.TranslatetoSnapshot_minimized();

                instance.painelDerrota.SetActive(true);
            }));
        }

        static IEnumerator Esperar(float tempo, Action Call)
        {
            yield return new WaitForSeconds(tempo);
            Call.Invoke();
        }

        public int GetBonusNoDamage()
        {
            if (NoDamage && ManagerScene.instance != null)
                return ManagerScene.instance.bonusNoDamage;

            return 0;
        }

        public int GetBonusNoDrone()
        {
            if (NoDrone && ManagerScene.instance != null)
                return ManagerScene.instance.bonusNoDrone;

            return 0;
        }

    }
}
