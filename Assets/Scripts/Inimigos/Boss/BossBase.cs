using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class BossBase : MonoBehaviour
    {
        [SerializeField] internal Rigidbody rig;
        [SerializeField] internal GameObject target;
        [SerializeField] private float tempoVibracaoMorte = 3;
        [SerializeField] internal Collider[] bossColissores;

        internal int idComportamento = 0;
        public List<BossComportamento> comportamentos;
        private bool bossAtivado;
        internal bool morto;

        public bool BossAtivado { get => bossAtivado; set => bossAtivado = value; }

        public virtual void Start()
        {
            foreach (var item in bossColissores)
                item.enabled = false;

            if (ManagerWarningUI.instance != null)
            {
                ManagerWarningUI.instance.OnComecaAnimacao();
                ManagerWarningUI.instance.OnFimWarning += OnFimWarning;
            }
            else
                print("ManagerWarningUI.instance == null");

            target?.SetActive(false);
        }

        internal void OnFimWarning()
        {
            target?.SetActive(true);
            StartCoroutine(AtivarBoss());
        }

        public virtual IEnumerator AtivarBoss()
        {
            while (!BossAtivado)
                yield return null;

            foreach (var item in bossColissores)
                item.enabled = true;

            if (comportamentos.Count > 0)
                comportamentos[idComportamento].Ativar(ProximoComportamento);
        }

        public void ProximoComportamento()
        {
            if (morto)
                return;

            idComportamento++;
            idComportamento = idComportamento < comportamentos.Count ? idComportamento : 0;
            comportamentos[idComportamento].Ativar(ProximoComportamento);
        }

        public virtual void Morreu()
        {
            GameStateController.instance.CallState(GlobalVariables.States.FINISH_STAGE.ToString());
            morto = true;
            ControleVibracao.Vibrar(tempoVibracaoMorte);
            GameManager.BlockPlayerController();
            foreach (var item in comportamentos)
            {
                item.StopAllCoroutines();
                item.Desativar();
            }

            foreach (var item in bossColissores)
                item.enabled = false;

            Tiro[] tiros = GameObject.FindObjectsOfType<Tiro>();
            foreach (var item in tiros)
            {
                item.Morrer(true);
            }
        }
    }
}
