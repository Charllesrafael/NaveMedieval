using UnityEngine;

namespace Nephenthesys
{
    public class DroneExtraCannons : Drone
    {
        public float tempoDelayTiroExtra = 0.1f;
        [SerializeField] private GameObject extracannons;

        public override void AtivarPoder()
        {
            base.AtivarPoder();
            ControllerPool.Create(extracannons, player.transform.position, Quaternion.identity);
            AcabouEfeito();

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Extra_Cannons", false);
        }
    }
}
