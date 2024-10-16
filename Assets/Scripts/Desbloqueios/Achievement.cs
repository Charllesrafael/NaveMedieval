using UnityEngine;
using Lean.Localization;
using System;
using UnityEngine.Events;

namespace Nephenthesys
{
    public class Achievement : ScriptableObject
    {
        public bool Unlocked;
        public AchievementConditionSettings achievementConditionSettings;
        public string nome;
        public string idLocalizacaoAchievementDesbloqueiado;
        [TextArea]
        public string _textoAchievementDesbloqueiado;

        internal string textoAchievementDesbloqueiado => LeanLocalization.GetTranslationText(idLocalizacaoAchievementDesbloqueiado, _textoAchievementDesbloqueiado);

        public void Initialize()
        {
            achievementConditionSettings.Initialize();
        }
    }
}
