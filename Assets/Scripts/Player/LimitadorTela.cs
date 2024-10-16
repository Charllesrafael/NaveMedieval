using System;
using UnityEngine;

namespace Nephenthesys
{
    public class LimitadorTela : MonoBehaviour
    {
        public static LimitadorTela instance;
        public float offsetScreen = 1;
        private Vector3 limiteBaixoEsquerda;
        private Vector3 limiteCimaDireita;

        public Vector3 LimiteBaixoEsquerda { get => limiteBaixoEsquerda; set => limiteBaixoEsquerda = value; }
        public Vector3 LimiteCimaDireita { get => limiteCimaDireita; set => limiteCimaDireita = value; }

        private void Awake()
        {
            instance = this;
            UpdateLimites();
        }

        public void UpdateLimites()
        {
            limiteCimaDireita = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 25));
            limiteBaixoEsquerda = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 25));
        }

        public Vector3 LimitarMovimentacao(Transform target, Vector3 vel, bool inverter = false, Action callBack = null)
        {
            Vector3 positcao = target.position;
            if (vel.x > 0 && positcao.x + (vel.x * Time.deltaTime) > limiteCimaDireita.x - offsetScreen)
            {
                positcao.x = limiteCimaDireita.x - offsetScreen;
                vel.x = inverter ? -vel.x : 0;
                callBack?.Invoke();
            }

            if (vel.x < 0 && positcao.x + (vel.x * Time.deltaTime) < limiteBaixoEsquerda.x + offsetScreen)
            {
                positcao.x = limiteBaixoEsquerda.x + offsetScreen;
                vel.x = inverter ? -vel.x : 0;
                callBack?.Invoke();
            }

            if (vel.z < 0 && positcao.z + (vel.z * Time.deltaTime) < limiteBaixoEsquerda.z + offsetScreen)
            {
                positcao.z = limiteBaixoEsquerda.z + offsetScreen;
                vel.z = inverter ? -vel.z : 0;
                callBack?.Invoke();
            }

            if (vel.z > 0 && positcao.z + (vel.z * Time.deltaTime) > limiteCimaDireita.z - offsetScreen)
            {
                positcao.z = limiteCimaDireita.z - offsetScreen;
                vel.z = inverter ? -vel.z : 0;
                callBack?.Invoke();
            }
            target.position = positcao;

            return vel;
        }

        public bool ForaCamera(Vector3 target)
        {
            return target.x > limiteCimaDireita.x || target.x < limiteBaixoEsquerda.x
            || target.z < limiteBaixoEsquerda.z || target.z > limiteCimaDireita.z;
        }
    }
}
