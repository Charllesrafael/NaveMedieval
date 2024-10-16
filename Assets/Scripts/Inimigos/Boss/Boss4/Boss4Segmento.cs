using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Segmento : MonoBehaviour
    {
        public Boss4 boss;
        public Transform calda;
        public State[] States;

        private Transform targetCurrent;
        private float lerpSeguirCurrent;
        private float lerpSeguirRotation;

        [System.Serializable]
        public struct State
        {
            public Transform target;
            public float lerpSeguir;
            public float lerpRotation;
        }

        public Transform Segue(Transform _target)
        {
            States[0].target = _target;
            targetCurrent = _target;
            return calda;
        }

        private void Update()
        {
            if (boss == null || States == null || States.Length == 0 || boss.GetState() < 0 || boss.GetState() >= States.Length)
                return;

            targetCurrent = States[boss.GetState()].target;
            lerpSeguirCurrent = States[boss.GetState()].lerpSeguir;
            lerpSeguirRotation = States[boss.GetState()].lerpRotation;

            if (targetCurrent == null)
                return;

            this.transform.position = Vector3.Lerp(this.transform.position, targetCurrent.transform.position, boss.vel * lerpSeguirCurrent * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetCurrent.transform.rotation, boss.vel * lerpSeguirRotation * Time.deltaTime);
        }
    }
}
