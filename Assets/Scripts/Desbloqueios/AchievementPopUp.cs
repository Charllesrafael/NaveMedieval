using UnityEngine;
using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Nephenthesys
{
    public class AchievementPopUp : MonoBehaviour
    {
        public static AchievementPopUp instance;
        [Multiline]
        public string pre_texto;
        public bool playOnEnable;
        public float tempoDelayTexto = 1;
        public UIView uiview;
        public Typist typist;
        private bool showing;
        private List<string> listTextos;

        public bool Showing { get => showing; set => showing = value; }

        private void Awake()
        {
            instance = this;
            listTextos = new List<string>();
        }

        private void OnEnable()
        {
            if (playOnEnable)
                PlayText(pre_texto);
        }

        private void OnDisable()
        {
            typist.TypeAll();
        }

        public void PlayText(string texto)
        {
            listTextos.Add(texto);
            if(!Showing)
            {
                Showing = true;
                StopAllCoroutines();
                uiview.Show();
                DigitarTexto();
            }
        }

        private void DigitarTexto()
        {
            string textoAtual = listTextos[0];
            listTextos.Remove(textoAtual);
            typist.ToType(textoAtual).OnEndTyping(() =>
            {
                StartCoroutine(Hide());
            });
        }

        IEnumerator Hide()
        {
            yield return new WaitForSeconds(tempoDelayTexto);
            
            if (listTextos.Count > 0)
                DigitarTexto();
            else
                uiview.Hide();
        }
    }
}
