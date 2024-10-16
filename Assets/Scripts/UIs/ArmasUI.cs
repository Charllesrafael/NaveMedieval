using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

namespace Nephenthesys
{
    public class ArmasUI : MonoBehaviour
    {
        [SerializeField] internal MMFeedbacks feedback;
        [SerializeField] internal Image drone;
        [SerializeField] internal Image[] lifes;

        internal bool noChanceDrone = false;

        public static ArmasUI instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            ChangeLive(GameManager.GetVidaInicial(), false);
        }

        public static void ChangeLive(int vida, bool useEffect = true)
        {
            if (instance == null)
                return;

            if (useEffect)
                instance.feedback?.PlayFeedbacks();

            for (int i = 0; i < instance.lifes.Length; i++)
            {
                instance.lifes[i].gameObject.SetActive(i < vida);
            }
            
            if(vida == 0)
                ActivateDrone(false, true);
        }

        public static void ActivateDrone(bool active = true, bool bypass = false)
        {
            if(!instance.noChanceDrone || bypass)
                instance.drone.gameObject.SetActive(active);
        }
    }
}
