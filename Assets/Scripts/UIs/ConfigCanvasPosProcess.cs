using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class ConfigCanvasPosProcess : MonoBehaviour
    {
        public Canvas canvas;
        public BoolRef posProcessUI;
        public int tentativas = 5;

        void Start()
        {
            if (posProcessUI != null && posProcessUI.value)
                StartCoroutine(ConfigCamera());
        }

        IEnumerator ConfigCamera()
        {
            int tentativa = 0;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.planeDistance = 0.5f;

            while (canvas.worldCamera == null && tentativa < tentativas)
            {
                yield return new WaitForSeconds(0.5f);
                canvas.worldCamera = Camera.main;
                tentativa++;
            }
        }
    }
}
