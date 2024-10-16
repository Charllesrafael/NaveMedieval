using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "AudioHolder", menuName = "Custom/AudioHolder", order = 0)]
    public class AudioHolder : ScriptableObject
    {
        public float volume_BGS;
        public float volume_SFX;

        public float Value_BGS { get => volume_BGS; set => this.volume_BGS = value; }
        public float Value_SFX { get => volume_SFX; set => this.volume_SFX = value; }
    }
}
