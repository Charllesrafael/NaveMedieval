using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneWater : Drone
    {
        [SerializeField] private float tempoVidaPoder = 3f;
        [SerializeField] private MorteDroneFire morteDroneFire;
        [SerializeField] private Vector3 posicaoPoder;
        [SerializeField] private GameObject visual;
        [SerializeField] private GameObject efeitoPoder;

        public override void AtivarPoder()
        {
            base.AtivarPoder();
            efeitoPoder.SetActive(true);
            visual.SetActive(false);
            GameManager.instance.temDrone = false;
            StartCoroutine(TempoVidaEfeito());

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Water_Power", false);
        }

        IEnumerator TempoVidaEfeito()
        {
            yield return new WaitForSecondsRealtime(tempoVidaPoder);
            this.AcabouEfeito();
            Destroy(efeitoPoder.gameObject);
        }

        public override Vector3 GetPositionDrone()
        {
            return player.transform.position + posicaoPoder;
        }

        public override void AcabouEfeito()
        {
            morteDroneFire.AplicarEfeito();
            base.AcabouEfeito();
        }
    }
}
