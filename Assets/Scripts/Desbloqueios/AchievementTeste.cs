using UnityEngine;

namespace Nephenthesys
{
    public class AchievementTeste : MonoBehaviour
    {
        public string[] NomeAchievement;
        public void DesbloquearNave(string nome)
        {
            AchievementController.instance.Unlock(nome);
        }
        public void BloquearNave(string nome)
        {
            AchievementController.instance.Lock(nome);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (var item in NomeAchievement)
                {
                DesbloquearNave(item);
                }
            }
        }
    }
}
