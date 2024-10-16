using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class Sort_Pitch : MonoBehaviour
    {
        public Vector2 range_Pitch_Random = new Vector2(0, 3);

        // Start is called before the first frame update
        void Start()
        {
            float v = Random.Range(range_Pitch_Random.x, range_Pitch_Random.y);
            GetComponent<AudioSource>().pitch = v;
        }


    }
}
