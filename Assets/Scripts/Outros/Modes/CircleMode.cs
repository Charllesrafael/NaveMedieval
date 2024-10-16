using UnityEngine;

namespace Nephenthesys
{
    public class CircleMode : TiroMode
    {
        [SerializeField] private float range = 50;
        [SerializeField] private float speed = 20;
        [SerializeField] private float subVel = 0.4f;
        [SerializeField] private float tempoVida = 0;
        private Tiro tiro;
        private Vector3 direcao = Vector3.right;

        private void Start()
        {
            tiro = GetComponent<Tiro>();
        }

        private void Update()
        {
            tempoVida += Time.deltaTime;
            direcao.x = Mathf.Cos(tempoVida * speed) * range * Time.deltaTime;
            direcao.z = Mathf.Sin(tempoVida * speed) * range * Time.deltaTime;
            direcao.z += subVel;
            tiro.direcaoMove = direcao;
        }
    }
}