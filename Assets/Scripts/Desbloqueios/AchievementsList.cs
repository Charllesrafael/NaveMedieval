
namespace Nephenthesys
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementsList")]
    public class AchievementsList : ScriptableObject
    {
        public List<Achievement> Achievements;
    }
}
