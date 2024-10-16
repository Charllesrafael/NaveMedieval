using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ControlerCenario : MonoBehaviour
    {
        public static ControlerCenario instance;
        public MoverCenario moverCenario;
        public float TempoNovoCenario = 2f;
        public Transform paiCenario;
        public BlocoCenario cenarioAtual;
        public GrupoCenarios[] _Cenarios;
        private int idGrupoCenarios = 0;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            CriarnovoBloco();
        }

        public void ProximaCenario()
        {
            idGrupoCenarios++;
            idGrupoCenarios = Mathf.Min(idGrupoCenarios, _Cenarios.Length - 1);
        }

        private BlocoCenario GetCenario()
        {
            if (!_Cenarios[idGrupoCenarios].UsouBlocoInicial)
            {
                _Cenarios[idGrupoCenarios].UsouBlocoInicial = true;
                return _Cenarios[idGrupoCenarios].BlocosInicial;
            }

            return _Cenarios[idGrupoCenarios].Blocos[UnityEngine.Random.Range(0, _Cenarios[idGrupoCenarios].Blocos.Length)];
        }

        public void CriarnovoBloco()
        {
            BlocoCenario cenarioAtualTemp = ControllerPool.Create(GetCenario(), cenarioAtual.Final.position, Quaternion.identity);
            cenarioAtualTemp.transform.parent = paiCenario;
            cenarioAtual = cenarioAtualTemp;
        }
    }
}
