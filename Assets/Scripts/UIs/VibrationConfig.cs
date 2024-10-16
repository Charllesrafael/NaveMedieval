using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

namespace Nephenthesys
{
    public class VibrationConfig : MonoBehaviour
    {
        public TextMeshProUGUI texto;
        public EnumSelection enumSelection;

        private void Start()
        {
            ControleVibracao.vibracaoLigada = PlayerPrefs.GetInt(enumSelection.saveString, 0) == 0 ? false : true;
            enumSelection.value = ControleVibracao.vibracaoLigada ? 1 : 0;
            texto.text = ControleVibracao.vibracaoLigada ? GetString("ON") : GetString("OFF");
        }

        private void OnEnable()
        {
            LocalizationConfig.instance?.AddChangeLocalization(AtualizarTexto);
        }

        private void OnDisable()
        {
            LocalizationConfig.instance?.RemoveChangeLocalization(AtualizarTexto);
        }

        public void ChangeVScync(int value)
        {
            ControleVibracao.vibracaoLigada = value == 0 ? false : true;
            PlayerPrefs.SetInt(enumSelection.saveString, value);
            texto.text = value == 0 ? GetString("OFF") : GetString("ON");
        }

        public void Change()
        {
            ChangeVScync(ControleVibracao.vibracaoLigada ? 0 : 1);
        }

        public void AtualizarTexto()
        {
            texto.text = ControleVibracao.vibracaoLigada ? GetString("ON") : GetString("OFF");
        }

        public string GetString(string texto)
        {
            return LeanLocalization.GetTranslationText(texto, texto);
        }
    }
}
