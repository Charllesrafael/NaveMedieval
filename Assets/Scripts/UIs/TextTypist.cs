using System.Net.Mime;
using System.Collections;
using TMPro;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

namespace Nephenthesys
{
    public class TextTypist : MonoBehaviour
    {
        public float tempoPorLetra = 0.1f;
        public TextMeshProUGUI textoUI;
        public GameObject setaProxima;
        public Image icone;
        public MMFeedbacks trocaIconeEfeito;
        public UIView uIView;
        public GameObject gameUI;

        public PlayAudio playAudio;
        public GameDialogList textHistoriaList;

        private int idCurrentText = 0;
        private bool prontoParaProximoTexto = false;
        private float tempoPorLetraTemp;

        void Awake()
        {
            if (ManagerScene.instance == null)
            {
                PulaHistoria();
                return;
            }

            ManagerScene.instance.IniciarHistoria += IniciarHistoria;
            icone.sprite = textHistoriaList.textHistorias[ManagerScene.instance.idFaseAtual].textos[idCurrentText].icone;
        }
        private void IniciarHistoria()
        {
            if (ManagerScene.instance.pulaHistoria)
                PulaHistoria();
            else
                uIView.Show();

            ManagerScene.instance.IniciarHistoria -= IniciarHistoria;
        }

        public void OnHistoria()
        {
            ProximoTexto(false);
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                if (!prontoParaProximoTexto)
                    PularDigitacao();
                else
                {
                    idCurrentText++;
                    if (idCurrentText < textHistoriaList.textHistorias[ManagerScene.instance.idFaseAtual].textos.Length)
                    {
                        prontoParaProximoTexto = false;
                        ProximoTexto();
                    }
                    else
                    {
                        setaProxima.SetActive(false);
                        uIView.Hide();
                    }
                }
            }
        }

        private void ProximoTexto(bool efeitoTroca = true)
        {
            if (efeitoTroca)
                trocaIconeEfeito?.PlayFeedbacks();
            icone.sprite = textHistoriaList.textHistorias[ManagerScene.instance.idFaseAtual].textos[idCurrentText].icone;
            // StartCoroutine(Digitar(textHistoriaList.textHistorias[ManagerScene.instance.idFaseAtual].textos[idCurrentText].texto));
            StartCoroutine(Digitar(textHistoriaList.GetTextHistorias(ManagerScene.instance.idFaseAtual, idCurrentText)));
        }

        public void PulaHistoria()
        {
            uIView.gameObject.SetActive(false);
            gameUI.SetActive(true);
            if (GameManager.instance != null)
                GameManager.instance.GameOn = true;
        }

        public void PularDigitacao()
        {
            tempoPorLetraTemp = 0;
        }

        IEnumerator Digitar(string texto)
        {
            tempoPorLetraTemp = tempoPorLetra;
            textoUI.text = "";

            setaProxima.SetActive(false);
            foreach (var item in texto)
            {
                yield return new WaitForSeconds(tempoPorLetraTemp);
                textoUI.text += item;
                if (tempoPorLetraTemp == 0)
                    break;
                playAudio?.Play();
            }
            setaProxima.SetActive(true);
            textoUI.text = texto;
            prontoParaProximoTexto = true;
        }
    }
}
