using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nephenthesys
{
    public class ControleVibracao : MonoBehaviour
    {
        public static ControleVibracao instance;
        private Coroutine coroutine;
        public static bool vibracaoLigada = true;
        public float minSpeed = 0.2f;
        public float maxSpeed = 1;

        private void Awake()
        {
            instance = this;
        }

        public static void Vibrar(float time)
        {
            if (instance == null || Gamepad.current == null || !vibracaoLigada)
                return;
            instance.StopAllCoroutines();
            instance.StartCoroutine(instance.Vibration(time));
        }

        private IEnumerator Vibration(float time)
        {
            Gamepad.current.SetMotorSpeeds(minSpeed, maxSpeed);
            yield return new WaitForSeconds(time);
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
    }
}
