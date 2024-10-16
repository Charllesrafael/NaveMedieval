using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class AudioTrigger : MonoBehaviour
    {
        //[SerializeField] string _name;
        [SerializeField] type audioTriggerType = type.boss;
        [SerializeField] screenType audioTriggerScreenType = screenType.menu;
        //[SerializeField] bool key = true;

        enum type
        {
            boss, screen, sfx
        }

        enum screenType
        {
            menu, game, gamewin, gameover, effectMouseEnter, effectMouseClick,
        }

        private void OnEnable()
        {
            Ignite();
        }


        /*public void IgniteInversed()
        {
            key = false; Ignite();
        }*/

        public void Ignite()
        {
            if (AudioController.Instance != null)
            {
                if (audioTriggerType == type.boss)
                {
                    AudioController.Instance.Boss();
                }

                /*if (audioTriggerType == type.sfx)
                {
                    AudioController.Instance.PlaySFX(_name, key);
                }*/

                if (audioTriggerType == type.screen)
                {
                    if (audioTriggerScreenType == screenType.menu) { AudioController.Instance.Menu(); }
                    if (audioTriggerScreenType == screenType.game) { AudioController.Instance.GameMode(); }
                    if (audioTriggerScreenType == screenType.gameover) { AudioController.Instance.GameOver(); }
                    if (audioTriggerScreenType == screenType.gamewin) { AudioController.Instance.GameWin(); }
                }
            }
            else
            {
                Debug.LogWarning("Singleton.Instance == null", this.gameObject);
            }
        }

    }
}
