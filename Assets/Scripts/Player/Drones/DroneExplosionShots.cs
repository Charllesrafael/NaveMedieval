using UnityEngine;

namespace Nephenthesys
{
    public class DroneExplosionShots : Drone
    {
        [SerializeField] private GameObject tiro;
        [SerializeField] private float quantidadeTiros = 30;

        public override void AtivarPoder()
        {
            base.AtivarPoder();
            float valorAngulo = 360/quantidadeTiros;
            Vector3 anguloAtual = Vector3.forward;

            for (int i = 0; i < quantidadeTiros; i++)
            {
                GameObject _tiro = ControllerPool.Create(tiro, this.transform.position, Quaternion.identity);
                anguloAtual.y = i*valorAngulo;
                _tiro.transform.eulerAngles = anguloAtual;
            }
            GameManager.instance.temDrone = false;
            AcabouEfeito();

            AudioSystem.instance?.AudioConfigSetTrigger("Drone_Explosion_Shots", false);
        }
    
    }
}
