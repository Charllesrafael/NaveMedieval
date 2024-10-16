using UnityEngine;

namespace Nephenthesys
{
    public class DroneEarth : Drone
    {
        [SerializeField]
        private GameObject shieldPrefab;
        public override void AtivarPoder()
        {
            base.AtivarPoder();
            GameObject shield = ControllerPool.Create(shieldPrefab, player.transform.position, Quaternion.identity);
            DroneShieldEarth droneShieldEarth = shield.GetComponentInChildren<DroneShieldEarth>();
            droneShieldEarth.drone = this;

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Earth_Power", false);
        }
    }
}
