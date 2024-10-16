using UnityEngine;

namespace Nephenthesys
{
    public class ArmaBase : MonoBehaviour
    {
        public IntRef LevelArma;
        public Drone drone;
        internal ControlerTiro ControlerTiro;
        [SerializeField] internal string[] soundShootsLevel;
        public virtual void Atirar()
        {

        }

        public Drone GetDrone()
        {
            return drone;
        }

        public string GetSoundName()
        {
            if(soundShootsLevel.Length > 0)
                return soundShootsLevel[LevelArma.value-1];

            Debug.LogWarning("soundShootsLevel.Length = 0, favor da uma olhada",this.gameObject);
            return "Shot1";
        }
    }
}
