using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Soundy;
using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "Audio_Scriptable_4_soundy", menuName = "Audio4Soudny/Audio_Scriptable_4_soundy")]
    public class AudioScriptObject : ScriptableObject
    {
        public string audio_shot_name;
        public bool musica;
        public bool isUsingSoundy = true;
        public SoundyData[] soundyDatas;
    }
}
