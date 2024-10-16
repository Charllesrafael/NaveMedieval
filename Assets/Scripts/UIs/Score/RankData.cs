using System.Collections.Generic;


namespace Nephenthesys
{

    using UnityEngine;

    [CreateAssetMenu(fileName = "RankData", menuName = "Nephenthesys/RankData", order = 0)]
    public class RankData : ScriptableObject
    {
        [System.Serializable]
        public struct keyValue
        {
            public string nome;
            public int score;
        }

        public List<keyValue> ListScore;

        private void Reset()
        {
            ListScore = new List<keyValue>();
            keyValue keyValue;
            keyValue.nome = "AAA";
            keyValue.score = 0;

            for (int i = 0; i < 10; i++)
                ListScore.Add(keyValue);


            PlayerPrefs.SetString("Rank", SetRankString(ListScore));
        }



        private string SetRankString(List<keyValue> rank)
        {
            string rankString = "";

            foreach (var item in rank)
            {
                rankString += "," + item.nome + "," + item.score;
            }

            return rankString;
        }
    }
}
