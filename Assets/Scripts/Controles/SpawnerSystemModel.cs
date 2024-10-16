using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "SpawnerSystem", menuName = "Nephenthesys/SpawnerSystem", order = 0)]
    public class SpawnerSystemModel : ScriptableObject
    {
        public Wave[] waves;

        [System.Serializable]
        public struct Wave
        {
            public bool chamaProximoTipoCenario;
            public float Delay;
            public WaveData waveData;
            public WaveStruct waveStruct;
        }

    }

    [System.Serializable]
    public struct Wave
    {
        public float Delay;
        public WaveStruct waveStruct;
        public WaveData waveData;
    }


    [System.Serializable]
    public struct WaveStruct
    {
        public List<DadosInimigo> dadosInimigos;
    }

    [System.Serializable]
    public struct DadosInimigo
    {
        public PathModel PrefabPathModel;
        [Tooltip("Prefeb de inimigo quando tem possibilidade de nao ter pathmodel, tipo algun boss")]
        public GameObject _Prefab;
        public int path;
        public float Delay;
    }
}
