using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Nephenthesys
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] internal float velocidadeMovimento;
        [SerializeField] private float velAniDroneAtivo = 15;
        [SerializeField] private float delayAtivacao = 2;
        [SerializeField] internal float tempoEfeito = 5;
        [SerializeField] internal Animator ani;
        [SerializeField] internal MMFeedbacks mMFeedbacks;
        [SerializeField] private GameObject fundoEfeito;
        [SerializeField] private GameObject fundoEscuro;
        [SerializeField] internal GameObject particulaMorte;
        [SerializeField] internal GameObject particulaAtivacao;
        [SerializeField] private AudioSource Drone_Charging;

        internal WaitForSecondsRealtime wait;
        internal PlayerVida player;
        private Vector3 pointDronePosition;
        private bool seguindoPlayer;
        private bool droneAtivado;

        public void OnValidate()
        {
            if (ani == null)
                ani = GetComponent<Animator>();
        }

        public virtual void Awake()
        {
            fundoEscuro.transform.parent = null;
            fundoEscuro.transform.position = Vector3.zero;
        }

        public virtual void Start()
        {
            player = GameManager.GetPlayer();

            GameManager.instance.temDrone = true;
            pointDronePosition = player.poitDrone.position;
            seguindoPlayer = true;
        }

        public virtual void Update()
        {
            this.transform.position = Vector3.Lerp(this.transform.position, pointDronePosition, velocidadeMovimento * Time.unscaledDeltaTime);
            if (ControllerInputs.instance.especial && GameManager.instance.GameOn)
                PreparandoPoder();
            else if (seguindoPlayer)
                pointDronePosition = player.poitDrone.position;
        }

        public void PreparandoPoder()
        {
            if (!seguindoPlayer)
                return;

            player.StopEffects();
            GameManager.instance.NoDrone = false;
            // Drone_Charging?.Play();
            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Charging", false);

            droneAtivado = true;
            seguindoPlayer = false;
            MMTimeManager.Instance.SetTimeScaleTo(0);
            fundoEfeito.SetActive(true);
            fundoEscuro.SetActive(true);
            ani.SetFloat("vel", velAniDroneAtivo);
            velocidadeMovimento = velocidadeMovimento / 2;
            pointDronePosition = GetPositionDrone();
            ArmasUI.ActivateDrone(false);
            StartCoroutine(DelayAtivacao());
        }

        public virtual Vector3 GetPositionDrone()
        {
            return new Vector3(Camera.main.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        }

        private IEnumerator DelayAtivacao()
        {
            yield return new WaitForSecondsRealtime(delayAtivacao);
            AtivarPoder();
        }

        public virtual void AtivarPoder()
        {
            fundoEfeito.SetActive(false);
        }

        public virtual void AcabouEfeito()
        {
            Morrer();
        }

        public void Destruir()
        {
            Destroy(fundoEscuro.gameObject);
            MMTimeManager.Instance.SetTimeScaleTo(1);
            ControllerPool.Discard(this.gameObject);
        }

        public void Morrer()
        {
            Destroy(fundoEscuro.gameObject);
            MMTimeManager.Instance.SetTimeScaleTo(1);
            if (particulaMorte && !droneAtivado)
                ControllerPool.Create(particulaMorte, this.transform.position, Quaternion.identity);
            else if (particulaAtivacao && droneAtivado)
                ControllerPool.Create(particulaAtivacao, this.transform.position, Quaternion.identity);

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Death", false);
            ControllerPool.Discard(this.gameObject);
        }
    }
}
