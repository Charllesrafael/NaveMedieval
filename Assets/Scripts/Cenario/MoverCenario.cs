using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class MoverCenario : MonoBehaviour
    {
        public float multiplicador = 1;
        public float velocidade = 1;
        public BlocoCenario blocoCenario;

        private void FixedUpdate()
        {
            this.transform.position += -this.transform.forward * velocidade * multiplicador * Time.deltaTime;
        }
    }
}
