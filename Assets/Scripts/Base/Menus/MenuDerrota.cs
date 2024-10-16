using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class MenuDerrota : MonoBehaviour
    {
        [SerializeField] private Button btRestart;
        [SerializeField] private Button btMenu;
        [SerializeField] internal TMP_InputField inputNameGameOver;
        [SerializeField] private GameObject BotoesGameOver;
        [SerializeField] internal MMFeedbacks unPause;
        private bool gameover;


        public void VerificaScore()
        {
            ExecuteEvents.Execute(inputNameGameOver.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            if (ScoreUI.instance == null || !ScoreUI.instance.EntraNoRank())
                AtivarBotoesGameOver(true);
            else if (ScoreUI.instance.EntraNoRank())
            {
                ExecuteEvents.Execute(inputNameGameOver.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                AtivarBotoesGameOver();
            }
        }

        public void AtivarBotoesGameOver()
        {
            AtivarBotoesGameOver(false);
        }

        public void AtivarBotoesGameOver(bool bypass = false)
        {
            if (!gameover)
            {
                gameover = true;
                AudioSystem.instance?.AudioConfigSetTrigger("Lose", false);
            }

            if (inputNameGameOver.text == "" && !bypass)
                return;

            inputNameGameOver.gameObject.SetActive(false);
            BotoesGameOver.SetActive(true);

            if (!bypass)
                ScoreUI.instance.SaveRank(inputNameGameOver.text);

            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(btRestart.gameObject);
        }

        public void Restart()
        {
            unPause?.PlayFeedbacks();

            if (!AudioSystem.instance.isPlayingBossBgm)
            { AudioSystem.instance.setRepeatBGS(true); }//<<<<<<<<<<<<<
            else { AudioSystem.instance.setRepeatBGS(false); }

            ManagerScene.instance.scoreValue.value = GameManager.instance.scoreAnterior;
            ManagerScene.instance.ReiniciarFase(true);
        }

        public void RestartGame()
        {
            unPause?.PlayFeedbacks();

            if (ManagerScene.instance.getIdFase() == 0) { AudioSystem.instance.setRepeatBGS(true); }
            else { AudioSystem.instance.setRepeatBGS(false); }

            ManagerScene.instance.ComecarJogo(true);
        }

        public void Menu()
        {
            unPause?.PlayFeedbacks();

            AudioSystem.instance.setRepeatBGS(false);
            /*if (!AudioSystem.instance.isPlayingBossBgm)
            { AudioSystem.instance.setRepeatBGS(false); }//<<<<<<<<<<<<<
            else { AudioSystem.instance.setRepeatBGS(false); }*/

            ManagerScene.instance.Menu(false);
        }
    }
}
