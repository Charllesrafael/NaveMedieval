using UnityEngine;

namespace Nephenthesys
{
    public class SpreadMode : TiroMode
    {
        [SerializeField] private float range = 1;

        private void Start()
        {
            if (TryGetComponent<Tiro>(out Tiro tiro))
            {
                tiro.direcaoMove.x = Random.Range(-range, range);
            }
        }
    }
}