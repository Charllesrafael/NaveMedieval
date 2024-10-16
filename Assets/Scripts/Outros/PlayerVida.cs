using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Nephenthesys
{
    public class PlayerVida : Vida
    {
        public float tempoVibracaoRecebeDano = 0.3f;
        public float tempoVibracaoMorte = 1.5f;
        [SerializeField] internal IntRef _vidaAtual;
        [SerializeField] internal Collider ColliderVida;
        [SerializeField] internal int vidaMax = 3;
        [SerializeField] internal ControlerTiro controlerTiro;
        [SerializeField] internal Transform poitDrone;
        [SerializeField] internal MMFeedbacks feedbackRecebeVida;
        [SerializeField] internal MMFeedbacks feedbackRecebeDano;
        [SerializeField] internal Action<int> OnRecebeVida;
        [SerializeField] internal Action<int,bool> OnRecebeDano;
        internal bool bypassDrone = false;

        internal Drone drone;

        public override void Awake()
        {
            base.Awake();
            _vidaAtual.value = Mathf.Max(vidaInicial, 1);
            OnRecebeVida += EventRecebeVida;
            OnRecebeDano += EventRecebeDano;
        }

        public override void RecebeDano(int _dano, bool ignoreThisBool = false, bool byPass = false)
        {
            if (!GameManager.instance.Playing || GameManager.instance.playerInvencivel)
                return;

            GameManager.instance.NoDamage = false;
            OnRecebeDano?.Invoke(_dano, ignoreThisBool);
        }

        private void EventRecebeDano(int _dano, bool ignoreThisBool)
        {
            if (Time.time > delayDanoCurrent + delayDano)
            {
                delayDanoCurrent = Time.time;
                feedBackDano?.Efeito();
                ControleVibracao.Vibrar(tempoVibracaoRecebeDano);

                if (vidaInicial == -1)
                    return;

                if (drone != null)
                {
                    drone.Morrer();
                    ArmasUI.ActivateDrone(false);
                    feedbackRecebeDano?.PlayFeedbacks();
                    GameManager.instance.temDrone = false;
                    drone = null;
                }
                else if (_vidaAtual.value - _dano > 0)
                {
                    AudioSystem.instance?.AudioConfigSetTrigger("HitPlayer", false);
                    _vidaAtual.value -= _dano;
                    feedbackRecebeDano?.PlayFeedbacks();
                }
                else
                {
                    _vidaAtual.value = 0;
                    Morrer(false);
                }
                ArmasUI.ChangeLive(_vidaAtual.value);
            }
        }

        public virtual void RecebeVida(int _vida)
        {
            OnRecebeVida?.Invoke(_vida);
        }

        private void EventRecebeVida(int _vida)
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Upgrade", false);
            if (_vidaAtual.value + _vida < vidaMax)
            {
                _vidaAtual.value += _vida;
            }
            else
            {
                if (_vidaAtual.value == vidaMax)
                    ObterDrone();
                else
                    _vidaAtual.value = vidaMax;
            }
            feedbackRecebeVida?.PlayFeedbacks();
            ArmasUI.ChangeLive(_vidaAtual.value);
        }

        public void ObterDrone()
        {
            if (bypassDrone && drone != null)
            {
                GameManager.instance.temDrone = false;
                drone.Destruir();
                drone = null;
            }

            if (drone == null && !GameManager.instance.temDrone)
            {
                drone = Instantiate(controlerTiro.GetArmaDrone());
                ArmasUI.ActivateDrone();
            }
        }

        public override void Morrer(bool ignorethisbool)
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Player_Explosion", false);
            ControleVibracao.Vibrar(tempoVibracaoMorte);
            GameManager.instance?.PerdeuOJogo();
            ArmasUI.ChangeLive(0);
            base.Morrer(ignorethisbool);
        }


        public override void OnTriggerEnter(Collider other)
        {
            Dangerous dangerous = other.GetComponent<Dangerous>();
            if (dangerous)
                RecebeDano(dangerous.DanoRef ? dangerous.DanoRef.value : 1, false);
        }

        public int GetVidaInicial()
        {
            return vidaInicial;
        }

        public void StopEffects()
        {
            feedbackRecebeVida.StopFeedbacks();
            feedbackRecebeDano.StopFeedbacks();
        }
    }
}
