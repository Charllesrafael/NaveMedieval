using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneAir : Drone
    {
        public override void AtivarPoder()
        {
            base.AtivarPoder();
            base.AcabouEfeito();
            GameManager.instance.temDrone = false;
            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Air_Power", false);
        }
    }
}
