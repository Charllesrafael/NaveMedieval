using UnityEngine;

using System.Collections.Generic;

namespace Nephenthesys
{
    public class DetertorInimigos: MonoBehaviour {
        private Transform targetProximo;
        public ArmaProjetilMissel targetSeguir;
        private List<Transform> listaInimigos;

        public Transform TargetProximo { 
            get 
            {
                listaInimigos.Remove(targetProximo);//teste
                return targetProximo;
            }
        }

        void Awake()
        {
            listaInimigos = new List<Transform>();
        }

        private void OnTriggerEnter(Collider other) {
            if(!listaInimigos.Contains(other.transform))
                listaInimigos.Add(other.transform);
        }
        
        private void OnTriggerStay(Collider other) {
            if(!listaInimigos.Contains(other.transform) && !LimitadorTela.instance.ForaCamera(other.gameObject.transform.position))
                listaInimigos.Add(other.transform);
        }

        private void OnTriggerExit(Collider other) {
            if(listaInimigos.Contains(other.transform))
                listaInimigos.Remove(other.transform);
        }

        private void FixedUpdate() {

            if(targetSeguir != null)
                this.transform.position = targetSeguir.transform.position;
            else
                Destroy(this.gameObject);


            if (listaInimigos.Count > 0)
                targetProximo = listaInimigos[0];

            if (listaInimigos.Count > 1) 
            {
                if(listaInimigos[0] == null || LimitadorTela.instance.ForaCamera(listaInimigos[0].position))
                {
                    listaInimigos.Remove(listaInimigos[0]);
                }
                else if(!LimitadorTela.instance.ForaCamera(listaInimigos[0].position))
                {
                    float distMin = Vector3.Distance(this.transform.position,listaInimigos[0].position);
                    for (int i = 1; i < listaInimigos.Count; i++)
                    {
                        if(listaInimigos[i] == null)
                        {
                            listaInimigos.Remove(listaInimigos[i]);
                            continue;
                        }

                        float distAtual = Vector3.Distance(this.transform.position,listaInimigos[i].position);
                        if(distAtual < distMin)
                        {
                            targetProximo = listaInimigos[i];
                            distMin = distAtual;
                        }
                    }
                }
            }
        }
    }
}
