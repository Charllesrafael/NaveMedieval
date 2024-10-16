using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneFire : Drone
    {
        [SerializeField] private GameObject explosao;
        [SerializeField] private int explosoesSimultanias = 3;
        [SerializeField] private float delayEntreExplosoes = 0.3f;
        [SerializeField] private MorteDroneFire morteDroneFire;
        private Vector3 LimiteCimaDireita;
        private Vector3 LimiteBaixoEsquerda;

        public override void Start()
        {
            base.Start();

            LimiteCimaDireita = LimitadorTela.instance.LimiteCimaDireita;
            LimiteBaixoEsquerda = LimitadorTela.instance.LimiteBaixoEsquerda;
            wait = new WaitForSecondsRealtime(delayEntreExplosoes);
        }

        public override void AcabouEfeito()
        {
            morteDroneFire.AplicarEfeito();
            base.AcabouEfeito();
        }

        public override void AtivarPoder()
        {
            base.AtivarPoder();
            GameManager.instance.temDrone = false;
            StartCoroutine(CriarExplosoes());
        }

        IEnumerator CriarExplosoes()
        {
            float tempoEfeitoInicial = Time.unscaledTime;
            while (Time.unscaledTime - tempoEfeito < tempoEfeitoInicial)
            {
                yield return wait;
                for (int i = 0; i < explosoesSimultanias; i++)
                {
                    ControllerPool.Create(explosao, GetRandomPosition(), Quaternion.identity);
                }
            }
            AcabouEfeito();
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                UnityEngine.Random.Range(LimiteCimaDireita.x, LimiteBaixoEsquerda.x),
                0,
                UnityEngine.Random.Range(LimiteCimaDireita.z, LimiteBaixoEsquerda.z)
            );
        }
    }
}
