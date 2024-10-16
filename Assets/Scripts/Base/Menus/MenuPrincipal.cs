using UnityEngine;
namespace Nephenthesys
{
    public class MenuPrincipal : MonoBehaviour
    {
        [SerializeField] private IntRef continues;
        private void Start()
        {
            continues.value = 3;
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void SelecaoPersonagem()
        {
            ManagerScene.instance.SelecaoNave();
        }

        public void Tutorial()
        {
            ManagerScene.instance.Tutorial();
        }
    }
}
