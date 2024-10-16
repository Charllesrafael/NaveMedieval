using System;
using Doozy.Engine.UI;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nephenthesys
{
    public class MenuPause : MonoBehaviour
    {
        public bool byPass;
        internal bool paused;

        public GameObject painelPause;

        public UIView painelBaseUIView;
        public UIView painelPauseUIView;
        public UIView painelopcaoUIView;

        [SerializeField] internal MMFeedbacks pause;
        [SerializeField] internal MMFeedbacks unPause;
        private bool resuming;

        private void Awake()
        {
            paused = false;
            if (ControllerInputs.instance && ControllerInputs.instance.playerInput != null)
                ControllerInputs.instance.playerInput.deviceLostEvent.AddListener(OnPause);
        }

        void OnDisable()
        {
            if (ControllerInputs.instance && ControllerInputs.instance.playerInput != null)
                ControllerInputs.instance.playerInput.deviceLostEvent.RemoveListener(OnPause);
        }

        private void OnPause(PlayerInput arg0)
        {
            Pause();
        }

        public void Pause()
        {
            if (paused)
                return;

            paused = true;
            pause?.PlayFeedbacks();
            painelPause.SetActive(true);
            painelBaseUIView.Show();
        }

        public void Resume()
        {
            if (resuming)
                return;

            resuming = true;
            unPause?.PlayFeedbacks();
            if (painelopcaoUIView.IsActive())
                painelopcaoUIView.Hide();
            painelPauseUIView.Hide();
            painelBaseUIView.Hide();
        }

        public void FimResume()
        {
            paused = false;
            resuming = false;
        }

        public void Menu()
        {
            unPause?.PlayFeedbacks();
            ManagerScene.instance.Menu(false);
        }

        public void Restart()
        {
            unPause?.PlayFeedbacks();
            //AudioSystem.instance.setRepeatBGS(true);
            if (!AudioSystem.instance.isPlayingBossBgm)
            { AudioSystem.instance.setRepeatBGS(true); }//<<<<<<<<<<<<<
            else { AudioSystem.instance.setRepeatBGS(false); }

            ManagerScene.instance.scoreValue.value = GameManager.instance.scoreAnterior;
            ManagerScene.instance.ReiniciarFase(true);
        }

        public void SelecionarNave()
        {
            unPause?.PlayFeedbacks();
            ManagerScene.instance.SelecaoNave();
        }

        void Update()
        {
            if (ControllerInputs.instance == null)
                return;

            if (ControllerInputs.instance.start && GameManager.instance.Playing && GameManager.instance.GameOn)
            {
                Pause();
            }
        }

        public void DesativarPainelPause()
        {
            if (painelBaseUIView.IsVisible && painelPauseUIView.IsHidden)
                painelPauseUIView.Show();
        }
    }
}