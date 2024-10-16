using UnityEngine;

namespace Nephenthesys
{
    public class Boss5BaseCorpo : MonoBehaviour
    {
        public Boss5 boss5;

        public void FimApareceu()
        {
            boss5.BossAtivado = true;
        }
    }
}
