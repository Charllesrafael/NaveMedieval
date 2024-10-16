using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Start : MonoBehaviour
    {
        public Boss4 boss;
        public Boss4Movimento boss4Movimento;
        public PathCreator pathCreator;
        public PathFollower pathFollower;
        public float distMin = 0.1f;
        private Vector3 endPoint;

        private void Start()
        {
            // endPoint = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1), this.transform.position) < distMin)
            {
                boss.BossAtivado = true;
                pathFollower.enabled = false;
                // boss4Movimento.movendo = true;
            }
        }
    }
}
