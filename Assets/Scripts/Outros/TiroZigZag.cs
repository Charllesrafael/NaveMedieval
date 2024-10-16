using UnityEngine;

namespace Nephenthesys
{
    public class TiroZigZag : Tiro
    {
        public float RangeZigZag = 1;
        public float SpeedZigZag = 1;
        public bool lookat;
        private float xInicial = 0;
        private float valorZigZag = 0;

        public void Config(float rangeZigZag, float speedZigZag, bool lookat)
        {
            RangeZigZag = rangeZigZag;
            SpeedZigZag = speedZigZag;
            this.lookat = lookat;
        }

        public override void Start()
        {
            base.Start();
            xInicial = direcaoMove.x;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            valorZigZag += Time.deltaTime;
            direcaoMove.x = (Mathf.Cos(valorZigZag * SpeedZigZag) * RangeZigZag);
            if (lookat)
                this.transform.LookAt(this.transform.position + direcaoMove);
        }
    }
}
