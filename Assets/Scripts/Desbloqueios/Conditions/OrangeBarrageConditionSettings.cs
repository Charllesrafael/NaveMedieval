using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementConditions/OrangeBarrageConditionSettings")]
    public class OrangeBarrageConditionSettings : AchievementConditionSettings
    {
        public override void Initialize()
        {
            GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
        }

        public void ConditionSettings()
        {
            if(ManagerScene.instance.idFaseAtual == 2 && ManagerScene.instance.naveSelecionada == 5)
            {
                AchievementController.instance.Unlock("Orange Barrage");
                GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
            }
        }
    }
}
