using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss4 : Boss
    {
        [SerializeField] private int State;
        public float vel = 50;
        public BossExplosao bossExplosao;
        public GameObject[] pecas;

        private void Awake() {
            bossExplosao.FimExplosoes += DestuirPecas;
        }

        private void DestuirPecas()
        {
            for (int i = pecas.Length - 1; i >= 0 ; i--)
            {
                Destroy(pecas[i].gameObject);
            }
        }

        internal void SetState(int _state)
        {
            State = _state;
        }

        internal int GetState()
        {
            return State;
        }
    }
}
