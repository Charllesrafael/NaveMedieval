using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nephenthesys
{
    public class StartBossAnimation : MonoBehaviour
    {
        public UnityEvent evento;

        public void CallAnimation()
        {
            evento?.Invoke();
        }
    }
}
