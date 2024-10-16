using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class audiosomporevento : MonoBehaviour
    {
        public void CrunchSound()
        {
            AudioSystem.instance.AudioConfigSetTrigger("MetalDrill", false);
        }
    }
}
