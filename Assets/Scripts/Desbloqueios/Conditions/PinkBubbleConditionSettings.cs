using UnityEngine;
    
namespace Nephenthesys
{
    [CreateAssetMenu(menuName = "Nephenthesys/AchievementConditions/PinkBubbleConditionSettings")]
    public class PinkBubbleConditionSettings : AchievementConditionSettings
    {
        public override void Initialize()
        {
            GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
        }

        public void ConditionSettings()
        {
            if(ManagerScene.instance.idFaseAtual == 1 && ManagerScene.instance.naveSelecionada == 4)
            {
                AchievementController.instance.Unlock("Pink Bubble");
                GameStateController.instance?.SubcribState(GlobalVariables.States.FINISH_STAGE.ToString(), ConditionSettings);
            }
        }
    }
}
