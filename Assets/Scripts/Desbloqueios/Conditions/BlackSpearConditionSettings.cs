using UnityEngine;
  
namespace Nephenthesys
{
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementConditions/BlackSpearConditionSettings")]
    public class BlackSpearConditionSettings : AchievementConditionSettings
    {
        public override void Initialize()
        {
            GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
        }

        public void ConditionSettings()
        {
            if(GameManager.instance.NoDrone && GameManager.instance.NoDamage)
            {
                AchievementController.instance.Unlock("Black Spear");
                GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
            }
        }
    }
}
