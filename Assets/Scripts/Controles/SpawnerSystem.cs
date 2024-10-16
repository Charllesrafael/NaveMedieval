using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace Nephenthesys
{
    public class SpawnerSystem : MonoBehaviour
    {
        public static SpawnerSystem instance;
        [SerializeField] private Transform pontoIncialInimigos;
        public SpawnerSystemModel spawnerSystemModel;
        public List<PathCreator> paths;
        private bool IniciouSpawner = false;
        private void Awake()
        {
            instance = this;
        }

        private IEnumerator Start()
        {
            while (GameManager.instance == null || !GameManager.instance.GameOn)
            {
                yield return new WaitForFixedUpdate();
            }
            StartSpawner();
        }

        public void StartSpawner()
        {
            if (IniciouSpawner)
                return;

            if (spawnerSystemModel == null)
            {
                Debug.Log("spawnerSystemModel esta nulo");
            }
            else
            {
                IniciouSpawner = true;
                StartCoroutine(IStartSpawner());
            }
        }

        IEnumerator IStartSpawner()
        {
            foreach (var wave in spawnerSystemModel.waves)
            {
                yield return new WaitForSeconds(wave.Delay);
                yield return StartCoroutine(CriarWave(wave));
            }
        }

        public IEnumerator CriarWave(SpawnerSystemModel.Wave wave)
        {

            if (wave.chamaProximoTipoCenario && ControlerCenario.instance != null)
                ControlerCenario.instance.ProximaCenario();

            if (wave.waveData != null)
            {
                foreach (var dadosInimigo in wave.waveData.dadosInimigos)
                {
                    yield return new WaitForSeconds(dadosInimigo.Delay);
                    CriarInimigo(dadosInimigo);
                }
            }
            else
            {
                foreach (var dadosInimigo in wave.waveStruct.dadosInimigos)
                {
                    yield return new WaitForSeconds(dadosInimigo.Delay);
                    CriarInimigo(dadosInimigo);
                }
            }
        }

        private void CriarInimigo(DadosInimigo dadosInimigo)
        {
            if (dadosInimigo.PrefabPathModel == null && dadosInimigo._Prefab == null)
                return;

            PathModel inimigoPathModel = null;

            if (dadosInimigo._Prefab != null)
            {
                GameObject inimigo = ControllerPool.Create(dadosInimigo._Prefab, pontoIncialInimigos.position, pontoIncialInimigos.rotation);
                inimigo.transform.parent = pontoIncialInimigos;
                inimigoPathModel = inimigo.GetComponent<PathModel>();
            }
            else
            {
                inimigoPathModel = ControllerPool.Create(dadosInimigo.PrefabPathModel, pontoIncialInimigos.position, pontoIncialInimigos.rotation);
            }

            if (inimigoPathModel != null)
            {
                inimigoPathModel.pathFollower.pathCreator = paths[dadosInimigo.path];
                inimigoPathModel.transform.parent = paths[dadosInimigo.path].transform;
            }
        }
    }
}