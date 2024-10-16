using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4Movimento : BossMovimento
    {
        public float lerpRotation = 0.2f;
        public bool movimentacaoAtivada;
        public bool ProximoPontoAutomatico;

        public GameObject alertaPrefab;
        public List<int> listaAlerta;

        public override void Start()
        {
            indoParaDireita = true;
        }
        public override void Movimentacao()
        {
            if (!movendo || pontosMovimento.Length == 0)
                return;

            if (this.transform.position != pontosMovimento[idPonto].position)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, pontosMovimento[idPonto].position, velocidadeMovimento * Time.deltaTime);
                this.transform.LookAt(
                    Vector3.MoveTowards(
                        this.transform.position + this.transform.forward
                        , new Vector3(pontosMovimento[idPonto].position.x, this.transform.position.y, pontosMovimento[idPonto].position.z)
                        , lerpRotation * Time.deltaTime
                    )
                );
            }
        }

        private void FixedUpdate()
        {
            if (!ProximoPontoAutomatico || !movendo)
                return;

            if (!TargetLonge(this.transform.position))
            {
                if (listaAlerta.Contains(idPonto))
                    Instantiate(alertaPrefab, pontosMovimento[idPonto].position, pontosMovimento[idPonto].rotation);
                ProximoPonto();
            }
        }
    }
}
