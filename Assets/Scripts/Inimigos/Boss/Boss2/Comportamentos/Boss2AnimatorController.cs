using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss2AnimatorController : MonoBehaviour
    {
        public Animator ani;
        public Action callBack;

        public void SetTrigger(string valor)
        {
            ani.SetTrigger(valor);
        }

        public void FimAnimacao()
        {
            callBack?.Invoke();
            callBack = null;
        }
    }
}
