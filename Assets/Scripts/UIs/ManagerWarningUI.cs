using System.Collections;
using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ManagerWarningUI : MonoBehaviour
    {
        public static ManagerWarningUI instance;
        public GameObject WarningPainel;
        public Action OnComecaAnimacao;
        public Action OnFimWarning;

        private void Awake()
        {
            instance = this;
            OnComecaAnimacao += ComecaAnimacao;
        }

        public float timeToStartBgmBossAndIntimidation = 3f;
        private void ComecaAnimacao()
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Alert", false);
            WarningPainel.SetActive(true);
            Invoke("triggerAudioBoss", timeToStartBgmBossAndIntimidation);
        }

        private int idfase;
        void triggerAudioBoss()
        {
            if (ManagerScene.instance)
                idfase = ManagerScene.instance.getIdFase();

            AudioSystem.instance?.TriggarAudioBosses(idfase);
        }
    }

}
