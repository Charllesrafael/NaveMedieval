using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss : BossBase
    {
        [SerializeField] private BossMovimento bossMovimento;

        public override void Morreu()
        {
            base.Morreu();
            bossMovimento?.Parar();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "StopBoss")
            {
                BossAtivado = true;
                bossMovimento.Mover();
                Destroy(other.gameObject);
            }
        }
    }
}
