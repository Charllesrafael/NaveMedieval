using UnityEngine;
using Lean.Localization;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "GameDialog", menuName = "Nephenthesys/GameDialog", order = 0)]
    public class GameDialog : ScriptableObject
    {
        public TrechoConversa[] textos;

        internal string GetTexto(int idCurrentText)
        {
            return textos[idCurrentText].GetTexto(this.name + "_" + idCurrentText);
        }

        [System.Serializable]
        public class TrechoConversa
        {
            public Sprite icone;
            [TextArea]
            public string texto;

            internal string GetTexto(string idLocalizacao)
            {
                return LeanLocalization.GetTranslationText(idLocalizacao, texto);
            }
        }
    }
}
