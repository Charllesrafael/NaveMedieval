using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneDoisLazerAtivo : MonoBehaviour
    {
        [SerializeField] private float tempoDoisLazer;
        [SerializeField] internal Vector3 rotacao;
        [SerializeField] internal float velocidadeRotacao;
        internal Drone drone;
        private PlayerVida player;

        IEnumerator Start()
        {
            player = GameManager.GetPlayer();

            yield return new WaitForSeconds(tempoDoisLazer);
            GameManager.instance.temDrone = false;
            Destroy(this.gameObject);
        }

        private void Update()
        {
            this.transform.position = player.transform.position;
            this.transform.Rotate(rotacao * velocidadeRotacao * Time.deltaTime);
        }
    }
}
