using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nephenthesys
{
    public class ControleSelecaoUI : MonoBehaviour
    {
        public EnumSelection enumSelection;
        [SerializeField] private SelectionAsset selectionAsset;


        [SerializeField] private GameObject icognita;
        [SerializeField] private GameObject icognitaMiniatura;
        [SerializeField] private Button botaoPlay;
        [SerializeField] private Button botaoBack;

        [SerializeField] private TextMeshProUGUI assetName;
        [SerializeField] private TextMeshProUGUI descricao;



        private int selected = 0;
        private int maxSelection = 4;
        private bool bypassSelected;

        private void Awake()
        {
            maxSelection = ManagerScene.instance.GetMaxNaves();
            enumSelection.maxValue = maxSelection;
        }

        public void Anterior()
        {
            if (LoadingManager.instance != null && LoadingManager.instance.loading)
                return;
            selected -= 1;
            Selecionar();
        }

        public void Proximo()
        {
            if (LoadingManager.instance != null && LoadingManager.instance.loading)
                return;
            selected += 1;
            Selecionar();
        }

        private void Selecionar()
        {
            if (selected > maxSelection - 1)
                selected = 0;
            else if (selected < 0)
                selected = maxSelection - 1;


            AudioSystem.instance?.AudioConfigSetTrigger("Enter", false);
            selectionAsset.SelectedItem(selected);
        }
        public void resetar_audio()
        {
            ManagerScene.instance.reset_audio_parameters_when_coming_from_menu();
        }

        public void Select()
        {
            if (EventSystem.current != null)
            {
                bypassSelected = true;
                EventSystem.current.SetSelectedGameObject(enumSelection.gameObject);
            }
        }

        public void OnSelected()
        {
            if (!bypassSelected)
            {
                AudioSystem.instance?.AudioConfigSetTrigger("Enter", false);
            }
            bypassSelected = false;
        }

        internal void MudarNave(NaveData naveAtual)
        {
            assetName.text = naveAtual.nome;
            botaoPlay.interactable = naveAtual.Unlocked;
            icognita.SetActive(!naveAtual.Unlocked);
            icognitaMiniatura.SetActive(!naveAtual.Unlocked);
            descricao.text = naveAtual.Descricao();
            Navigation navigation = enumSelection.navigation;
            navigation.selectOnDown = naveAtual.Unlocked ? botaoPlay : botaoBack;
            enumSelection.navigation = navigation;
        }
    }
}
