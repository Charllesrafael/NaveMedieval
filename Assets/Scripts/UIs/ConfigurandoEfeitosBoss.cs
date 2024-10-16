using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Nephenthesys
{
    public class ConfigurandoEfeitosBoss : MonoBehaviour
    {
        public static ConfigurandoEfeitosBoss instance;
        public MMFeedbacks flash;
        public MMFeedbacks shakeExplosao;

        public Action explosao;
        public Action flashExplosao;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {

            explosao += () =>
            {
                shakeExplosao?.PlayFeedbacks();
            };


            flashExplosao += () =>
            {
                flash?.PlayFeedbacks();
            };
        }
    }
}
