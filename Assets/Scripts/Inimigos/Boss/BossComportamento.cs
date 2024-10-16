using System;
using UnityEngine;

namespace Nephenthesys
{
    public abstract class BossComportamento : MonoBehaviour
    {
        public abstract void Ativar(Action onFimComportamento = null);
        public Action FimComportamento;
        public virtual void Desativar() { }
    }
}
