using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

namespace Nephenthesys
{
    public class LocalizationConfig : MonoBehaviour
    {
        public static LocalizationConfig instance;

        public TextMeshProUGUI texto;
        public EnumSelection enumSelection;
        public Action OnChangeLanguage;

        public void AddChangeLocalization(Action _function)
        {
            OnChangeLanguage += _function;
        }

        public void RemoveChangeLocalization(Action _function)
        {
            OnChangeLanguage -= _function;
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }

            instance = this;
        }

        private void Start()
        {
            // ControleVibracao.vibracaoLigada = PlayerPrefs.GetInt(enumSelection.saveString, 0) == 0 ? false : true;
            // enumSelection.value = ControleVibracao.vibracaoLigada ? 1 : 0;

            texto.text = LeanLocalization.Instances[0].CurrentLanguage;
            texto.text = texto.text != "Portuguese" ? texto.text : "Português";
            OnChangeLanguage?.Invoke();
        }

        private void OnEnable()
        {
            enumSelection.SetValueWithoutNotify(LeanLocalization.Instances[0].CurrentLanguage != "Portuguese" ? 0 : 1);
        }

        public void ChangeLocalization(int value)
        {
            if (value == 0)
                SetLanguage("English");
            else
                SetLanguage("Portuguese");

            texto.text = value == 0 ? "English" : "Português";
            OnChangeLanguage?.Invoke();
        }

        public void Change()
        {
            enumSelection.value = enumSelection.value == 1 ? 0 : 1;
            ChangeLocalization(enumSelection.value);
        }

        public void SetLanguage(string newLanguage)
        {
            LeanLocalization.Instances[0].SetCurrentLanguage(newLanguage);
        }
    }
}
