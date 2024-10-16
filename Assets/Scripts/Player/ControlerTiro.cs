using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ControlerTiro : MonoBehaviour
    {
        [SerializeField] private bool tutorial;
        [SerializeField] private ArmaBase armaAtiva;
        [SerializeField] internal ListaArmas listaArmas;
        [SerializeField] private Transform baseArma;
        [SerializeField] private int idArma;

        private void Start()
        {
            if (tutorial)
                idArma = 0;
            else if (ManagerScene.instance != null)
                idArma = ManagerScene.instance.GetAtualArmaId();

            SetArma(idArma);
        }

        public void SetArma(int id)
        {
            if (id == -1)
                id = 0;

            if (armaAtiva != null)
                Destroy(armaAtiva.gameObject);

            id = Mathf.Clamp(id, 0, listaArmas.armas.Length - 1);
            idArma = id;
            armaAtiva = ControllerPool.Create(listaArmas.armas[id], baseArma.position, baseArma.rotation);
            armaAtiva.ControlerTiro = this;
            armaAtiva.transform.parent = baseArma;
        }

        private void Update()
        {
            if (GameManager.instance == null || !GameManager.instance.GameOn | !GameManager.instance.Playing)
                return;

            if (Time.timeScale > 0 && armaAtiva != null && ControllerInputs.instance.atirar)
            {
                armaAtiva.Atirar();
            }
        }

        internal Drone GetArmaDrone()
        {
            return armaAtiva.GetDrone();
        }

        internal int GetIdArma()
        {
            return idArma;
        }



        public void ShotAudioTrigger(int levelArma)
        {
            AudioSystem.instance?.ShotAudioManager(idArma, levelArma, false);
        }



    }
}
