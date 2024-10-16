using System;
using PathCreation.Examples;
using UnityEngine;

namespace Nephenthesys
{
    public class BossMovimento : MonoBehaviour
    {
        [SerializeField] internal PathFollower pathFollower;
        [SerializeField] internal float velocidadeMovimento;
        [SerializeField] internal float minDist = 0.1f;
        [SerializeField] internal Transform[] pontosMovimento;

        internal bool movendo = false;
        internal bool indoParaDireita;
        internal int idPonto;
        [SerializeField] internal bool loopMovimento = false;

        public virtual void Start()
        {
            indoParaDireita = UnityEngine.Random.Range(0, 100) > 50;
        }

        public virtual void Update()
        {
            if (movendo)
                Movimentacao();
        }

        public void SetPoints(Transform[] _pontos)
        {
            idPonto = 0;
            pontosMovimento = _pontos;
        }

        public virtual void Movimentacao()
        {
            if (this.transform.position != pontosMovimento[idPonto].position)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pontosMovimento[idPonto].position, velocidadeMovimento * Time.deltaTime);
        }

        internal void Mover()
        {
            if (pathFollower) pathFollower.enabled = false;
            movendo = true;
        }

        internal void Parar()
        {
            if (pathFollower) pathFollower.enabled = false;
            movendo = false;
        }

        internal int GetIdPonto()
        {
            return idPonto;
        }

        public void SetIdPonto(int id)
        {
            idPonto = Mathf.Clamp(id, 0, pontosMovimento.Length - 1);
        }

        internal Vector3 GetDirecao(Vector3 position)
        {
            return (pontosMovimento[idPonto].position - position).normalized;
        }

        internal bool TargetLonge(Vector3 position, int id = -1)
        {
            if (id == -1)
                id = idPonto;

            return Vector3.Distance(position, pontosMovimento[id].position) > minDist;
        }

        internal virtual void ProximoPonto()
        {
            if (indoParaDireita)
            {
                if (idPonto < pontosMovimento.Length - 1)
                {
                    idPonto++;
                }
                else
                {
                    if (!loopMovimento)
                    {
                        idPonto--;
                        indoParaDireita = false;
                    }
                    else
                    {
                        idPonto = 0;
                    }
                }
            }
            else
            {
                if (idPonto > 0)
                {
                    idPonto--;
                }
                else
                {
                    idPonto++;
                    indoParaDireita = true;
                }
            }
        }
    }
}
