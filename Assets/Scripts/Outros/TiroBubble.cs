
using System;
using UnityEngine;

namespace Nephenthesys
{
    public class TiroBubble : Tiro
    {
        public int quantidadeQuicadas = 2;
        public override void FixedUpdate()
        {
            direcaoMove = LimitadorTela.instance.LimitarMovimentacao(this.transform, direcaoMove, true, Boucing);
            base.FixedUpdate();
        }

        public void Boucing()
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Bounce_Bubble", false);
        }
        
        public override void OnTriggerEnter(Collider other)
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Bounce_Bubble", false);
        }

        void OnCollisionEnter(Collision other)
        {
            if(quantidadeQuicadas > 0)
            {
                quantidadeQuicadas--;
                Quicar(other);
                AudioSystem.instance?.AudioConfigSetTrigger("Bounce_Bubble", false);
            }else
            { 
                Morrer(destruirAoColidir);
            }
        }

        private void Quicar(Collision other)
        {
            float forca = direcaoMove.magnitude;
            Vector3 vetorRenteAColisao = Vector3.Cross(direcaoMove, new Vector3(other.contacts[0].normal.x,0,other.contacts[0].normal.z));
            Vector3 vetorRefletido = Vector3.Cross(vetorRenteAColisao, direcaoMove);
            direcaoMove = vetorRefletido.normalized * forca;
        }
    }
}
