using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class ContinueController : MonoBehaviour
    {
        [SerializeField] private GameObject painelContinue;
        [SerializeField] private GameObject painelGameOver;
        [SerializeField] private TextMeshProUGUI contador;
        [SerializeField] private TextMeshProUGUI numeroContinues;
        [SerializeField] private MMFeedbacks mmfeedback;
        [SerializeField] private MenuDerrota menuDerrota;

        public float tempoDelay = 2f;
        private Coroutine IContar;
        private int currentCont = 9;
        private bool ClicouEmContinue = false;

        private void StartContinue()
        {
            numeroContinues.text = "x " + ManagerScene.instance.continues.ToString();
            contador.text = "9";
            mmfeedback?.PlayFeedbacks();
            IContar = StartCoroutine(Contar());
        }

        IEnumerator Contar(int valorInicial = 9)
        {
            currentCont = valorInicial;
            if (currentCont > -1)
                yield return new WaitForSecondsRealtime(tempoDelay);
            while (currentCont > 0)
            {
                currentCont--;
                AudioSystem.instance?.AudioConfigSetTrigger("ContinueContagem", false);
                mmfeedback?.PlayFeedbacks();
                contador.text = currentCont.ToString();
                yield return new WaitForSecondsRealtime(tempoDelay);
            }

            contador.text = "0";

            painelGameOver.SetActive(true);
            if (!ScoreUI.instance.EntraNoRank())
            {
                menuDerrota.AtivarBotoesGameOver(true);
            }
            else
            {
                ExecuteEvents.Execute(menuDerrota.inputNameGameOver.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                menuDerrota.AtivarBotoesGameOver();
            }
            painelContinue.SetActive(false);
        }

        private void Update()
        {
            if (ClicouEmContinue || painelGameOver.activeSelf)
                return;

            if ((ControllerInputs.instance.atirar || ControllerInputs.instance.start) && painelContinue.activeSelf && ManagerScene.instance.continues > 0)
            {
                ClicouEmContinue = true;
                StopAllCoroutines();
                // StopCoroutine(IContar);
                ManagerScene.instance.continues--;
                AudioSystem.instance?.AudioConfigSetTrigger("ContinueConfirmacao", false);
                menuDerrota.Restart();
            }
            else if (Input.anyKeyDown && currentCont > -1)
            {
                currentCont--;
                AudioSystem.instance?.AudioConfigSetTrigger("ContinueContagem", false);
                
                StopAllCoroutines();
                // StopCoroutine(IContar);
                IContar = StartCoroutine(Contar(currentCont));

                mmfeedback?.PlayFeedbacks();
                contador.text = currentCont.ToString();
            }
        }

        public void VerificarContinues()
        {
            if (ManagerScene.instance.continues <= 0)
            {
                painelGameOver.SetActive(true);
                menuDerrota.VerificaScore();
                painelContinue.SetActive(false);
            }
            else
            {
                painelContinue.SetActive(true);
                StartContinue();
            }
        }
    }
}
