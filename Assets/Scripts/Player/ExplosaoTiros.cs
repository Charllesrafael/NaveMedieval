using UnityEngine;

namespace Nephenthesys
{
    public class ExplosaoTiros : MonoBehaviour
    {
        [SerializeField] private Tiro tiro;
        public Transform[] pontos;

        private void Start()
        {
            foreach (var item in pontos)
            {
                ControllerPool.Create(tiro, item.position, item.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
