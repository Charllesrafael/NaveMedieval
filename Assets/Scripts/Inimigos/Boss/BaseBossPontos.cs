using UnityEngine;

namespace Nephenthesys
{
    public class BaseBossPontos : MonoBehaviour
    {
        void Start()
        {
            this.transform.parent = null;
            this.transform.position = Vector3.zero;
            this.transform.eulerAngles = Vector3.zero;
        }
    }
}
