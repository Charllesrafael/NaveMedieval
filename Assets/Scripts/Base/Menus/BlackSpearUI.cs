using UnityEngine;

namespace Nephenthesys
{
    public class BlackSpearUI : MonoBehaviour
    {
        [SerializeField] internal Color droneColor;

        void Start()
        {
            droneColor = new Color32( 0xFF , 0x55 , 0x55 , 0xFF );
            ArmasUI.instance.drone.color = droneColor;
            ArmasUI.instance.noChanceDrone = true;

            for (int i = 1; i < ArmasUI.instance.lifes.Length; i++)
                ArmasUI.instance.lifes[i].color = droneColor;
        }
    }
}
