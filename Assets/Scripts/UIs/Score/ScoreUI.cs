using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace Nephenthesys
{
    public class ScoreUI : MonoBehaviour
    {
        public static ScoreUI instance;
        public IntRef scoreValue;
        public TextScoreUI textScoreUI;
        public DataBaseScore dataBaseScore;
        public MMFeedbacks feedback;
        public TextMeshProUGUI score;

        public int ScoreValue
        {
            get => scoreValue.Value;
            set
            {
                scoreValue.Value = value;
                feedback?.PlayFeedbacks();
                UpdateUI();
            }
        }

        private void Awake()
        {
            instance = this;
            UpdateUI();
        }

        public static void CriarTextUIScore(int id, Vector3 _position)
        {
            if (id == -1 || instance == null)
                return;

            TextScoreUI _textScoreUI = ControllerPool.Create(instance.textScoreUI, _position);
            _textScoreUI.score.text = instance.dataBaseScore.scores[id].ToString();
            Add(instance.dataBaseScore.scores[id]);
        }

        public static void Add(int value)
        {
            instance.ScoreValue += value;
            GameStateController.instance?.CallState(GlobalVariables.States.UPDATE_SCORE.ToString());
        }

        private void UpdateUI()
        {
            if (Rank.instance != null)
                score.text = ScoreValue.ToString(Rank.instance.pattern);
            else
                score.text = ScoreValue.ToString("000000000");
        }

        public void SaveRank(string nome = "NAME")
        {
            if (Rank.instance == null || ScoreValue <= 0)
                return;

            Rank.instance.AddScore(nome, ScoreValue);
            Rank.instance.GravarRank();
        }

        public bool EntraNoRank()
        {
            if (Rank.instance == null || ScoreValue <= 0)
                return false;

            return Rank.instance.EntraNoRank(ScoreValue);
        }
    }
}
