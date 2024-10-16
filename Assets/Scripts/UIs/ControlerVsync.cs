using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

namespace Nephenthesys
{
    public class ControlerVsync : MonoBehaviour
    {
        public TextMeshProUGUI texto;
        public EnumSelection enumSelection;

        private void OnEnable()
        {
            LocalizationConfig.instance?.AddChangeLocalization(AtualizarTexto);
            QualitySettings.vSyncCount = PlayerPrefs.GetInt(enumSelection.saveString, 0);
            enumSelection.value = QualitySettings.vSyncCount;
            AtualizarTexto();
        }

        private void OnDisable()
        {
            LocalizationConfig.instance?.RemoveChangeLocalization(AtualizarTexto);
        }

        public void ChangeVScync(int value)
        {
            QualitySettings.vSyncCount = value;
            PlayerPrefs.SetInt(enumSelection.saveString, QualitySettings.vSyncCount);
            AtualizarTexto();
        }

        public void Change()
        {
            ChangeVScync(QualitySettings.vSyncCount == 0 ? 1 : 0);
        }

        public string GetString(string texto)
        {
            return LeanLocalization.GetTranslationText(texto, texto);
        }

        public void AtualizarTexto()
        {
            texto.text = QualitySettings.vSyncCount == 1 ? GetString("ON") : GetString("OFF");
        }
    }
}
