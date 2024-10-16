using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class AchievementController : MonoBehaviour
    {
        public static AchievementController instance;
        public AchievementsList AchievementsList;
        private Dictionary<string, Achievement> AchievementsMap;

        private void Awake()
        {
            instance = this;

            foreach (var achievement in AchievementsList.Achievements)
                achievement.Unlocked = PlayerPrefs.GetInt("unlocked-" + achievement.nome, 0) == 1;
                
            AchievementsMap = new Dictionary<string, Achievement>();
            foreach (var item in AchievementsList.Achievements)
                AchievementsMap.Add(item.nome, item);
        }

        private void Start() {
            foreach (var item in AchievementsMap.Values)
            {
                item.Initialize();
            }
        }

        public bool IsBlocked(string nomeAchievement)
        {
            if(AchievementsMap.ContainsKey(nomeAchievement))
                return !AchievementsMap[nomeAchievement].Unlocked;
                
            return false;
        }

        public void Unlock(string nomeAchievement)
        {
            if (AchievementsMap.ContainsKey(nomeAchievement) && !AchievementsMap[nomeAchievement].Unlocked)
            {
                AchievementsMap[nomeAchievement].Unlocked = true;
                PlayerPrefs.SetInt("unlocked-" + AchievementsMap[nomeAchievement].nome, 1);
                AchievementPopUp.instance?.PlayText(AchievementsMap[nomeAchievement].nome+" "+ Lean.Localization.LeanLocalization.GetTranslationText("Unloked", "Unloked"));  
            }
        }

        public void Lock(string nomeAchievement)
        {
            if (AchievementsMap.ContainsKey(nomeAchievement))
            {
                AchievementsMap[nomeAchievement].Unlocked = false;
                PlayerPrefs.SetInt("unlocked-" + AchievementsMap[nomeAchievement].nome, 0);
            }
        }
    }
}
