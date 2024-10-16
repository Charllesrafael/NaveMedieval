using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class ExtraCannons : MonoBehaviour{
        
        [SerializeField] private float tempoExtraCannons;
        [SerializeField] private float delayTiroExtraCannons = 0.05f;
        [SerializeField] internal TiroMisselPlayer tiro;

        [SerializeField] internal Transform[] pontosLevel;
        private PlayerVida player;
        private ArmaProjetilMissel armaProjetilMissel;
        private float delayTiroSave;

        IEnumerator Start()
        {
            player = GameManager.GetPlayer();
            armaProjetilMissel = FindObjectOfType<ArmaProjetilMissel>();
            armaProjetilMissel.OnFire += OnFire;
            delayTiroSave = armaProjetilMissel.delayTiro;
            armaProjetilMissel.delayTiro = delayTiroExtraCannons;

            yield return new WaitForSeconds(tempoExtraCannons);
            armaProjetilMissel.OnFire -= OnFire;
            armaProjetilMissel.delayTiro = delayTiroSave;
            GameManager.instance.temDrone = false;
            Destroy(this.gameObject);
        }

        private void Update()
        {
            this.transform.position = player.transform.position;
        }

        public void OnFire()
        {
            foreach (var item in pontosLevel)
            {
                TiroMisselPlayer tiroMissel = ControllerPool.Create(tiro, item.position, item.rotation);
                tiroMissel.armaProjetilMissel = armaProjetilMissel;
            }
        }
    }
}
