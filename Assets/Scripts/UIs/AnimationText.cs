using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nephenthesys
{
    public class AnimationText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI texto;
        public float tempoEspera = 0.3f;

        private void Start()
        {
            StartCoroutine(AnimarTexto());
        }

        private IEnumerator AnimarTexto()
        {
            while (true)
            {
                texto.text = texto.text.Insert(texto.text.Length - 1, texto.text[0].ToString());
                texto.text = texto.text.Remove(0, 1);
                yield return new WaitForSeconds(tempoEspera);
            }
        }
    }
}
