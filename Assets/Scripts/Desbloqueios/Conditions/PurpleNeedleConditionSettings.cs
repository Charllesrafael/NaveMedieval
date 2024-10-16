using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementConditions/PurpleNeedleConditionSettings")]
    public class PurpleNeedleConditionSettings : AchievementConditionSettings
    {
        public int valorMeta = 5000;
        public override void Initialize()
        {
            GameStateController.instance?.SubcribState(GlobalVariables.States.UPDATE_SCORE.ToString(), ConditionSettings);
        }

        public void ConditionSettings()
        {
            if(ScoreUI.instance.ScoreValue >= valorMeta)
            {
                AchievementController.instance.Unlock("Purple Needle");
                GameStateController.instance?.UnsubcribState(GlobalVariables.States.UPDATE_SCORE.ToString(), ConditionSettings);
            }
        }
    }
}
