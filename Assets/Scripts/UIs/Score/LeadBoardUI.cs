using TMPro;
using UnityEngine;

namespace Nephenthesys
{
    public class LeadBoardUI : MonoBehaviour
    {
        public TextMeshProUGUI nomes;
        public TextMeshProUGUI numeros;

        private void OnEnable()
        {
            if (Rank.instance == null)
                return;

            nomes.text = Rank.instance.GetNameList();
            numeros.text = Rank.instance.GetValueList();
        }
    }
}
