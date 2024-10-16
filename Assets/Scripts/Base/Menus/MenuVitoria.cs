using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class MenuVitoria : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textoScore;
        [SerializeField] private TextMeshProUGUI textoNoDamage;
        [SerializeField] private TextMeshProUGUI textoNoDrone;
        [SerializeField] private TextMeshProUGUI textoScoreTotal;
        [SerializeField] private TMP_InputField inputNameGameOver;

        [SerializeField] internal MMFeedbacks unPause;
        [SerializeField] private int somaScore = 10;
        [SerializeField] private float tempoContagem = 0.1f;
        [SerializeField] private float tempoProximoFase = 2;
        private bool comecouContagem = false;
        private int scoreTotal = 0;

        public void NextStage()
        {
            comecouContagem = false;
            unPause?.PlayFeedbacks();
            ManagerScene.instance.ProximaFase();
        }

        public void Menu()
        {
            unPause?.PlayFeedbacks();
            ManagerScene.instance.Menu(false);
        }

        public void ComecarContagem()
        {
            if (comecouContagem)
                return;

            comecouContagem = true;
            StartCoroutine(IComecarContagem());
        }

        IEnumerator IComecarContagem()
        {
            textoScore.text = ScoreUI.instance.ScoreValue.ToString("000000000");
            textoNoDamage.text = GameManager.instance.GetBonusNoDamage().ToString("000000000");
            textoNoDrone.text = GameManager.instance.GetBonusNoDrone().ToString("000000000");

            scoreTotal = 0;
            yield return IContagem(textoScore, ScoreUI.instance.ScoreValue);
            yield return IContagem(textoNoDamage, GameManager.instance.GetBonusNoDamage());
            yield return IContagem(textoNoDrone, GameManager.instance.GetBonusNoDrone());

            ScoreUI.instance.ScoreValue = scoreTotal;
            StartCoroutine(INextStage());
        }

        IEnumerator IContagem(TextMeshProUGUI scoreTextMesh, int _scoreTotal)
        {
            int scoreTotalOld = _scoreTotal;
            int scoreAtual = 0;
            while (scoreAtual < _scoreTotal)
            {
                yield return new WaitForSecondsRealtime(tempoContagem);

                scoreTotalOld -= somaScore;
                scoreTextMesh.text = scoreTotalOld.ToString("000000000");
                scoreAtual += somaScore;
                scoreTotal += somaScore;
                textoScoreTotal.text = scoreTotal.ToString("000000000");
            }
        }

        private void Update()
        {
            if (Input.anyKeyDown && comecouContagem)
            {
                comecouContagem = false;
                StopAllCoroutines();

                int scoreAtual = ScoreUI.instance.ScoreValue;
                scoreAtual += GameManager.instance.GetBonusNoDamage();
                scoreAtual += GameManager.instance.GetBonusNoDrone();

                textoScore.text = "000000000";
                textoNoDamage.text = "000000000";
                textoNoDrone.text = "000000000";
                textoScoreTotal.text = scoreAtual.ToString("000000000");
                ScoreUI.instance.ScoreValue = scoreAtual;
                StartCoroutine(INextStage());
            }
        }

        IEnumerator INextStage()
        {
            if (ManagerScene.instance.idFaseAtual >= ManagerScene.instance.scenas.Length - 1)
            {
                inputNameGameOver.gameObject.SetActive(true);
                ExecuteEvents.Execute(inputNameGameOver.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
            else
            {
                yield return new WaitForSecondsRealtime(tempoProximoFase);
                if (ManagerScene.instance.idFaseAtual < ManagerScene.instance.scenas.Length - 1)
                {
                    NextStage();
                }
            }
        }

        public void SalvarScore()
        {
            inputNameGameOver.gameObject.SetActive(false);
            ScoreUI.instance.SaveRank(inputNameGameOver.text);
            NextStage();
        }
    }
}
