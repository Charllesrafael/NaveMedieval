using UnityEngine;
using Doozy.Engine.Progress;
using Doozy.Engine.UI;

namespace Nephenthesys
{
    public class BarraVidaBoss : MonoBehaviour
    {
        public static BarraVidaBoss instance;
        public Progressor progressor;
        public UIView uIView;

        void Awake()
        {
            instance = this;
        }

        public void Aparecer(int vidaInicial)
        {
            uIView.Show();
            progressor.SetMax(vidaInicial);
            progressor.SetValue(vidaInicial);
        }

        public void Desaparecer()
        {
            uIView.Hide();
        }

        public void SetProgress(int vida)
        {
            progressor.SetValue(vida);
        }

    }
}
