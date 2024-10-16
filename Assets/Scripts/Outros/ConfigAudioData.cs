using Doozy.Engine.Soundy;
using UnityEngine;

namespace Nephenthesys
{
    // [RequireComponent(typeof(AudioSource))]
    public class ConfigAudioData : MonoBehaviour
    {
        [SerializeField] private AudioSource audiosource;
        [SerializeField] private SoundyData soundyDatas;

        private void OnValidate()
        {
            if (audiosource == null)
            {
                audiosource = GetComponent<AudioSource>();
                audiosource.enabled = false;
            }
        }

        private void Start()
        {
            ConfigAudio();
            audiosource.enabled = true;
        }

        private void ConfigAudio()
        {
            SoundGroupData soundGroupData = soundyDatas.GetAudioData();

            audiosource.clip = soundGroupData.GetAudioData(soundyDatas.GetAudioData().Mode).AudioClip;
            audiosource.outputAudioMixerGroup = SoundyManager.Database.GetSoundDatabase(soundyDatas.DatabaseName).OutputAudioMixerGroup;
            audiosource.loop = soundGroupData.Loop;
            audiosource.pitch = soundGroupData.RandomPitch;
            audiosource.volume = soundGroupData.RandomVolume;
            audiosource.priority = soundGroupData.Priority;
        }
    }
}
