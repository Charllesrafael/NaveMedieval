using System.Collections;
using Doozy.Engine.Progress;
using UnityEngine;
using Lean.Localization;
using System;

namespace Nephenthesys
{
    public class ControleHistoriaInicial : MonoBehaviour
    {
        public bool onStart;
        public bool fimHistoria;
        public float delayStart;
        public float delayEntreTextos = 1f;

        public float tepoPulaHistoria = 2;
        private float tepoPulaHistoriaAtual = 0;
        public Typist typist;
        public Progressor progressorSkip;

        private int idTextCurrent = 0;

        [System.Serializable]
        private struct TextoData
        {
            [SerializeField] internal string refLocalizacao;
            [TextArea]
            [SerializeField] internal string texto;
        }

        [SerializeField] private TextoData[] textoData;

        private void Awake()
        {
            progressorSkip?.SetMax(tepoPulaHistoria);
        }

        private IEnumerator Start()
        {
            if (onStart)
            {
                yield return new WaitForSeconds(delayStart);
                OnStartTyping();
            }
        }

        public void OnStartTyping()
        {
            typist.ToType(GetTextoLocalization(idTextCurrent)).OnEndTyping(() =>
            {
                StartCoroutine(IOnStartTyping());
            });
        }

        private string GetTextoLocalization(int id)
        {
            if (id < textoData.Length)
                return LeanLocalization.GetTranslationText(textoData[id].refLocalizacao, textoData[id].texto);

            return "";
        }

        IEnumerator IOnStartTyping(bool esperar = true)
        {
            if (esperar)
                yield return new WaitForSeconds(delayEntreTextos);
            idTextCurrent++;
            if (idTextCurrent < textoData.Length)
                OnStartTyping();
            else
            {
                if (fimHistoria)
                {
                    ManagerScene.instance.Menu(false);
                }
                else
                {
                    ManagerScene.instance.ComecarJogo(false);
                }
            }
        }

        private void Update()
        {
            if (fimHistoria || LoadingManager.instance.loading)
                return;

            if (Input.anyKey)
                tepoPulaHistoriaAtual += Time.deltaTime;
            else
                tepoPulaHistoriaAtual = 0;

            PulaTexto();

            progressorSkip?.SetValue(tepoPulaHistoriaAtual);

            if (tepoPulaHistoriaAtual >= tepoPulaHistoria)
            {
                ManagerScene.instance.ComecarJogo(false);
                fimHistoria = true;
            }
        }

        private void PulaTexto()
        {
            if (Input.anyKeyDown)
            {
                if (typist.IsTyping())
                    typist.TypeAll();
                else
                    StartCoroutine(IOnStartTyping(false));
            }
        }
    }
}
