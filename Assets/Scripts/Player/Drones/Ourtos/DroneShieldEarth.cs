using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneShieldEarth : MonoBehaviour
    {
        [SerializeField] private Animator ani;
        [SerializeField] private float tempoShield;
        internal PlayerVida player;
        [SerializeField] internal GameObject target;
        [SerializeField] internal DroneEarth drone;
        [SerializeField] internal GameObject particulaMorte;

        IEnumerator Start()
        {
            player = GameManager.GetPlayer();

            yield return new WaitForSeconds(tempoShield);
            if (tempoShield > 0)
            {
                ani.SetTrigger("Fim");
                AudioSystem.instance?.AudioConfigSetTrigger("Drone_Earth_Power_Deactivate", false);
            }
        }

        private void Update()
        {
            target.transform.position = player.transform.position;
        }

        public void FimAparecer()
        {
            drone.AcabouEfeito();
            GameManager.instance.playerInvencivel = true;
        }

        public void FimShield()
        {
            if (particulaMorte)
                ControllerPool.Create(particulaMorte, target.transform.position, Quaternion.identity);

            GameManager.instance.temDrone = false;
            GameManager.instance.playerInvencivel = false;

            Destroy(target.gameObject);
        }
    }
}
