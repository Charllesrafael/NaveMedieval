using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nephenthesys
{
    public class SliderUpdadeVol : MonoBehaviour
    {
        Slider _slider;
        public bool isMaster;
        public bool isBGS;
        public bool isSFX;

        private void Start()
        {
            _slider = GetComponent<Slider>();

            if (isBGS) _slider.value = AudioController.Instance.audio_scriptable.volume_BGS;
            if (isSFX) _slider.value = AudioController.Instance.audio_scriptable.volume_SFX;
            slider_update();
        }

        public void slider_update()
        {
            if (isMaster)   AudioController.Instance.change_Master  (_slider.value);
            if (isBGS)      AudioController.Instance.change_BGS     (_slider.value);
            if (isSFX)      AudioController.Instance.change_SFX     (_slider.value);
        }


    }
}
