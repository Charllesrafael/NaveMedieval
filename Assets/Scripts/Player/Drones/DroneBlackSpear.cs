using UnityEngine;

namespace Nephenthesys
{
    public class DroneBlackSpear : Drone
    {
        [SerializeField]
        private GameObject extraLazersPrefab;
        public override void AtivarPoder()
        {
            base.AtivarPoder();
            ControllerPool.Create(extraLazersPrefab, player.transform.position, Quaternion.identity);
            AcabouEfeito();
            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Black_Spear", false);
        }
    }
}
