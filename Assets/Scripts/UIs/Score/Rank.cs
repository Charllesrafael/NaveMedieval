using System.Collections;
using System.Collections.Generic;


namespace Nephenthesys
{

    using UnityEngine;

    public class Rank : MonoBehaviour
    {
        public static Rank instance;
        public int maxScores = 10;
        public RankData rankData;
        public string pattern = "00000000";
        public string ListaPattern = "------------------------";

        public List<KeyValuePair<string, int>> _rank;

        private void Awake()
        {
            if (instance != null)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

        }

        private void Start()
        {
            if (PlayerPrefs.GetString("Rank", "") != "")
                _rank = GetRank(PlayerPrefs.GetString("Rank"));
            else
                AddRankScore();
        }

        public void UpdateRank()
        {
            if (PlayerPrefs.GetString("Rank", "") != "")
                _rank = GetRank(PlayerPrefs.GetString("Rank"));

            AddRankScore(false);
        }

        [ContextMenu("RESET-RANK")]
        public void AddResetRank()
        {
            AddRankScore();
        }

        public void AddRankScore(bool _reset = true)
        {
            if (_rank == null)
                _rank = new List<KeyValuePair<string, int>>();

            if (_reset)
                _rank.Clear();

            foreach (var item in rankData.ListScore)
                AddScore(item.nome, item.score);

            GravarRank();
        }

        public void ResetRank()
        {
            _rank.Clear();
            PlayerPrefs.SetString("Rank", "");
        }

        public void AddScore(string nome, int score)
        {
            if (_rank == null)
                _rank = new List<KeyValuePair<string, int>>();

            _rank.Add(new KeyValuePair<string, int>(nome, score));
        }

        private void Ordenar()
        {
            _rank.Sort(
                delegate (KeyValuePair<string, int> firstPair,
                KeyValuePair<string, int> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );

            if (_rank.Count > rankData.ListScore.Count)
            {
                _rank.RemoveRange(rankData.ListScore.Count, _rank.Count - rankData.ListScore.Count);
            }
        }

        public bool EntraNoRank(int valor)
        {
            return valor > _rank[_rank.Count - 1].Value;
        }

        public void GravarRank()
        {
            Ordenar();
            PlayerPrefs.SetString("Rank", SetRankString(_rank));
        }

        private string SetRankString(List<KeyValuePair<string, int>> rank)
        {
            string rankString = "";

            foreach (var item in rank)
            {
                rankString += "," + item.Key + "," + item.Value;
            }

            return rankString;
        }

        private List<KeyValuePair<string, int>> GetRank(string rankString)
        {
            List<KeyValuePair<string, int>> rankReturn = new List<KeyValuePair<string, int>>();

            string[] listaDados = rankString.Split(',');

            for (int i = 1; i < listaDados.Length; i += 2)
            {
                rankReturn.Add(new KeyValuePair<string, int>(listaDados[i], int.Parse(listaDados[(i + 1)])));
            }

            return rankReturn;
        }

        public string GetNameList()
        {
            string texto = "";

            for (int i = 0; i < _rank.Count; i++)
            {
                if (i == 0)
                    texto += _rank[i].Key;
                else
                    texto += "\n" + _rank[i].Key;
            }

            return texto;
        }

        public string GetValueList()
        {
            string texto = "";

            for (int i = 0; i < _rank.Count; i++)
            {
                if (i == 0)
                    texto += _rank[i].Value.ToString(pattern);
                else
                    texto += "\n" + _rank[i].Value.ToString(pattern);
            }

            return texto;
        }

        public override string ToString()
        {
            string texto = "";

            for (int i = 0; i < _rank.Count; i++)
            {
                if (i == 0)
                    texto += FormatText(_rank[i].Key, _rank[i].Value);
                else
                    texto += "\n" + FormatText(_rank[i].Key, _rank[i].Value);
            }

            return texto;
        }

        private string FormatText(string textoStart, int _textoEnd)
        {
            string _texto = ListaPattern;

            string textoEnd = _textoEnd.ToString(pattern);

            _texto = _texto.Substring(textoStart.Length, _texto.Length - textoStart.Length);
            _texto = _texto.Substring(textoEnd.Length, _texto.Length - textoEnd.Length);

            _texto = _texto.Insert(0, textoStart);
            _texto = _texto.Insert(_texto.Length, textoEnd);

            return _texto;
        }


        // [ContextMenu("add RANK")]
        // public void SaveRank()
        // {
        //     int valor = Random.Range(50, 9999);
        //     print("add Rank >> " + valor);
        //     Rank.instance.AddScore("AAA", valor);
        //     Rank.instance.GravarRank();
        // }

    }
}
