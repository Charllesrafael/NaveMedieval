using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementConditions/WhitePrismConditionSettings")]
    public class WhitePrismConditionSettings : AchievementConditionSettings
    {
        public override void Initialize()
        {
            GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
        }

        public void ConditionSettings()
        {
            if(ManagerScene.instance.idFaseAtual == ManagerScene.instance.scenas.Length-1)
            {
                AchievementController.instance.Unlock("White Prism");
                GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
            }
        }
    }
}
