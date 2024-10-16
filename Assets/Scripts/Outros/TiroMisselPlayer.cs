using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class TiroMisselPlayer : TiroMissel
    {        
        public ArmaProjetilMissel armaProjetilMissel;
        public override void Start()
        {
            target = armaProjetilMissel.DetectorAtual.TargetProximo;
            _vel = velInicialMissel;
            StartCoroutine(IStart());
        }

        public override void Update()
        {
            if(ativado)
            {
                if (target != null && !LimitadorTela.instance.ForaCamera(target.position))
                {
                    direcaoMove = Vector3.Lerp(direcaoMove, (target.transform.position - this.transform.position).normalized, lerpDelay * Time.deltaTime);
                    this.transform.LookAt(this.transform.position + direcaoMove);
                }else if(target == null)
                {
                    target = armaProjetilMissel.DetectorAtual.TargetProximo;
                }
            }
        }
    }
}
