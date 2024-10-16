using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class AudioControllerSystemSpanwer : MonoBehaviour
    {
        public GameObject AudioSystemController_Prefab;
        // Start is called before the first frame update
        void Start()
        {
            GameObject.Instantiate(AudioSystemController_Prefab);
        }
    }
}
