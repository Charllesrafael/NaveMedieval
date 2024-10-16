using UnityEngine;
using UnityEngine.Events;

namespace Nephenthesys
{
    public class Boss4AlertaFaixa : MonoBehaviour
    {
        public GameObject pai;
        public UnityEvent Acabou;

        public void OnAcabou()
        {
            Acabou?.Invoke();
            Destroy(pai.gameObject);
        }
    }
}
