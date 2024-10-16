using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class TiroMissel : Tiro
    {
        public float tempoDelayAtivacaoMissel = 1f;
        public float velInicialMissel = 1f;
        public float lerpDelay;
        internal Transform target;
        internal bool ativado;
        internal float _vel;

        public override void Start()
        {
            _vel = velInicialMissel;
            target = GameManager.GetPlayer().transform;
            StartCoroutine(IStart());
        }

        public IEnumerator IStart()
        {
            direcaoMove = this.transform.forward;

            yield return new WaitForSeconds(tempoDelayAtivacaoMissel);
            ativado = true;
            _vel = tiroModel.Velocidade;

        }

        public override void FixedUpdate()
        {
            transform.position += direcaoMove * _vel * Time.deltaTime;
        }

        public virtual void Update()
        {
            if (target != null && ativado)
            {
                direcaoMove = Vector3.Lerp(direcaoMove, (target.transform.position - this.transform.position).normalized, lerpDelay * Time.deltaTime);
                this.transform.LookAt(this.transform.position + direcaoMove);
            }
        }
    }
}