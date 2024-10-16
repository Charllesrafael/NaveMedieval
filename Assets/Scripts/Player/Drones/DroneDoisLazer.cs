using UnityEngine;

namespace Nephenthesys
{
    public class DroneDoisLazer : Drone
    {
        [SerializeField] private GameObject doisLazers;

        public override void AtivarPoder()
        {
            base.AtivarPoder();
            GameObject _doisLazers = ControllerPool.Create(doisLazers, player.transform.position, Quaternion.identity);
            DroneDoisLazerAtivo droneDoisLazerAtivo = _doisLazers.GetComponentInChildren<DroneDoisLazerAtivo>();
            droneDoisLazerAtivo.drone = this;
            AcabouEfeito();

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Dois_Lazer", false);
        }
    }
}
