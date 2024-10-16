using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class ControleSelecao : MonoBehaviour
    {
        [SerializeField] private ListaArmas listaArmas;
        [SerializeField] private Transform PaiNaves;
        [SerializeField] private Transform PaiDrones;
        private List<GameObject> navesCriadasNaCena;
        private List<Drone> dronesCriadasNaCena;

        private void Awake()
        {
            ConfigurarNavesNaCena();
        }

        private void ConfigurarNavesNaCena()
        {
            List<NaveData> naves = ManagerScene.instance.GetAllNavesPrefabs();
            navesCriadasNaCena = new List<GameObject>();
            dronesCriadasNaCena = new List<Drone>();

            foreach (var nave in naves)
            {
                navesCriadasNaCena.Add(ControllerPool.Create(nave.GetNavePrefab(), PaiNaves));
                DesativarComponents(navesCriadasNaCena[navesCriadasNaCena.Count - 1]);
                navesCriadasNaCena[navesCriadasNaCena.Count - 1].SetActive(false);

                Drone drone = listaArmas.GetDrone(nave.GetArmaId());
                if (drone != null)
                {
                    dronesCriadasNaCena.Add(ControllerPool.Create(drone, PaiDrones));
                    DesativarComponents(dronesCriadasNaCena[dronesCriadasNaCena.Count - 1].gameObject);
                    dronesCriadasNaCena[dronesCriadasNaCena.Count - 1].gameObject.SetActive(false);
                }
                else
                    dronesCriadasNaCena.Add(null);
            }
        }

        private void DesativarComponents(GameObject obj)
        {
            MonoBehaviour[] monoBehaviours = obj.GetComponents<MonoBehaviour>();
            foreach (var item in monoBehaviours)
                item.enabled = false;

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
                animator.enabled = false;
        }

        internal void MudarNave(int naveId)
        {
            for (int i = 0; i < navesCriadasNaCena.Count; i++)
            {
                navesCriadasNaCena[i].SetActive(naveId == i);
                if (dronesCriadasNaCena[i] != null)
                    dronesCriadasNaCena[i].gameObject.SetActive(naveId == i);
            }
        }
    }
}
