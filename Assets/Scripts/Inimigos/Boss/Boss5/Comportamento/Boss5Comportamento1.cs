using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss5Comportamento1 : BossComportamento
    {
        [SerializeField] private float delay;
        [SerializeField] private float delayEntreTiros;
        [SerializeField] private int quantidadeSalvaTirosMax;
        private int quantidadeSalvaTiros;
        private WaitForSeconds waitForSeconds;

        [SerializeField] private ArmaProjetil armaProjetil;
        [SerializeField] private float delayLazerMira;
        [SerializeField] private GameObject[] lazerMira;
        [SerializeField] private GameObject[] lazerMaior;
        [SerializeField] private SpawnerSystemModel spawnerSystemModel;
        private Coroutine comportamento;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(armaProjetil.delayTiro);
        }

        public override void Ativar(Action onFimComportamento = null)
        {
            comportamento = StartCoroutine(IStartSpawner(onFimComportamento));
        }

        IEnumerator IAtivarLazer()
        {
            foreach (var item in lazerMira)
                item?.SetActive(true);

            yield return new WaitForSeconds(delayLazerMira);

            foreach (var item in lazerMaior)
                item?.SetActive(true);
        }

        IEnumerator IStartSpawner(Action onFimComportamento)
        {
            yield return new WaitForSeconds(delay);

            if (lazerMira.Length > 0)
                StartCoroutine(IAtivarLazer());

            if (quantidadeSalvaTirosMax > 0)
                StartCoroutine(IAtirar());

            foreach (var wave in spawnerSystemModel.waves)
            {
                yield return new WaitForSeconds(wave.Delay);
                yield return StartCoroutine(SpawnerSystem.instance.CriarWave(wave));
            }

            foreach (var item in lazerMira)
                item?.SetActive(false);

            foreach (var item in lazerMaior)
                item?.SetActive(false);

            onFimComportamento?.Invoke();
        }

        IEnumerator IAtirar()
        {
            while (comportamento != null)
            {
                quantidadeSalvaTiros = 0;
                while (quantidadeSalvaTiros < quantidadeSalvaTirosMax)
                {
                    yield return waitForSeconds;
                    armaProjetil?.Atirar();
                    quantidadeSalvaTiros++;
                }
                yield return new WaitForSeconds(delayEntreTiros);
            }
        }

        public override void Desativar()
        {
            StopAllCoroutines();
            // StopCoroutine(comportamento);
            foreach (var item in lazerMira)
                item?.SetActive(false);

            foreach (var item in lazerMaior)
                item?.SetActive(false);
        }
    }
}
