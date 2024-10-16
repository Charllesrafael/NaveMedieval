using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class ControleTutorial : MonoBehaviour
    {
        [System.Serializable]
        public class FaseTutorial
        {
            public bool concluido = false;
            public bool desativarAoConluir = false;
            public string[] listaInputs;
            public GameObject[] objetos;
        }

        public static ControleTutorial instance;
        public bool inicializado = false;
        public float delayVoltaMenu = 3f;
        public PlayerVida playerVida;

        public FaseTutorial[] fasesTutorial;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            ControllerInputs.instance?.ConfigInputsBasicos(fasesTutorial[0].listaInputs);
        }

        private void Inicializar()
        {
            ProximoTutorial(0);
        }

        private void Update()
        {
            if (!GameManager.instance.GameOn)
                return;

            if (!inicializado)
            {
                inicializado = true;
                Inicializar();
            }

            if (ControllerInputs.instance.movimento.x > 0 && !fasesTutorial[1].concluido)
            {
                ProximoTutorial(1);
            }

            if (ControllerInputs.instance.atirar && !fasesTutorial[2].concluido)
            {
                ProximoTutorial(2);
                playerVida?.RecebeVida(3);
            }

            if (ControllerInputs.instance.especial && !fasesTutorial[3].concluido)
            {
                ProximoTutorial(3);
                StartCoroutine(VoltarMenu());
            }
        }

        private void ProximoTutorial(int id)
        {
            fasesTutorial[id].concluido = true;
            if (fasesTutorial[id].desativarAoConluir)
            {
                foreach (var item in fasesTutorial[id].objetos)
                    item.SetActive(false);
            }

            if ((id + 1) < fasesTutorial.Length)
            {
                ControllerInputs.instance.ConfigInputsBasicos(fasesTutorial[(id + 1)].listaInputs);
                foreach (var item in fasesTutorial[(id + 1)].objetos)
                    item.SetActive(true);
            }
        }

        private IEnumerator VoltarMenu()
        {
            yield return new WaitForSeconds(delayVoltaMenu);
            ManagerScene.instance.Menu(false);
        }
    }
}
