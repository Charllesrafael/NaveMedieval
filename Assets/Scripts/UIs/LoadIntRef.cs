using TMPro;
using UnityEngine;

namespace Nephenthesys
{
    public class LoadIntRef : MonoBehaviour
    {
        public IntRef valor;
        public TextMeshProUGUI textmeshprougui;

        void Start()
        {
            if (Rank.instance != null)
                textmeshprougui.text = valor.value.ToString(Rank.instance.pattern);
            else
                textmeshprougui.text = valor.value.ToString("000000000");
        }
    }
}
