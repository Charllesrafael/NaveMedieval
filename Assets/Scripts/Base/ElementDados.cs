using UnityEngine;
using Lean.Localization;

namespace Nephenthesys
{
    [System.Serializable]
    public class ElementDados
    {
        public string idLocalizacao;
        public string idLocalizacaoUnlock;

        internal string Descricao(bool unlocked)
        {
            if (!unlocked)
                return LeanLocalization.GetTranslationText(idLocalizacaoUnlock, ComoDesbloquear);

            return LeanLocalization.GetTranslationText(idLocalizacao, _Descricao);
        }

        [TextArea]
        public string ComoDesbloquear;

        [TextArea]
        public string _Descricao;
    }
}
