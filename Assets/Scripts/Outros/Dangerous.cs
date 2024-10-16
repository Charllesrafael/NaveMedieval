using UnityEngine;

namespace Nephenthesys
{
    public class Dangerous : MonoBehaviour
    {
        public IntRef DanoRef;
        public bool DestroiOther = false;

        private void OnTriggerEnter(Collider other)
        {
            if (DestroiOther)
            {
                if (other.transform.parent != null)
                    Destroy(other.transform.parent.gameObject);
                else
                    Destroy(other.gameObject);
            }
        }
    }
}