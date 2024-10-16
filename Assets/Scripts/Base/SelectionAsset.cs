using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nephenthesys
{
    public class SelectionAsset : MonoBehaviour
    {
        [SerializeField] private ControleSelecao controleSelecao;
        [SerializeField] private ControleSelecaoUI controleSelecaoUI;
        private bool voltaMenu;
        private NaveData naveAtual;
        private List<NaveData> listaNaves;

        private void Awake()
        {
            listaNaves = ManagerScene.instance.totemData.listaNaves;
        }

        void Start()
        {
            SelectedItem(0);
        }

        public void SelectedItem(int choice)
        {
            ManagerScene.instance.naveSelecionada = choice;
            naveAtual = listaNaves[choice];

            controleSelecao.MudarNave(choice);
            controleSelecaoUI.MudarNave(naveAtual);
            ManagerScene.instance.naveSelecionada = choice;
        }

        public void AssetSelecionado()
        {
            if (LoadingManager.instance.loading)
                return;

            if (!naveAtual.Unlocked)
            {
                AudioSystem.instance?.AudioConfigSetTrigger("Error", false);
                return;
            }

            AudioSystem.instance?.AudioConfigSetTrigger("Click", false);
            ManagerScene.ComecarHistoria();
        }

        public void VoltarMenu()
        {
            if (LoadingManager.instance.loading)
                return;

            ManagerScene.instance.Menu(false);
        }

        void Update()
        {
            if (LoadingManager.instance.loading)
                return;

            if (ControllerInputs.instance.cancel && !voltaMenu)
            {
                voltaMenu = true;
                VoltarMenu();
            }
        }
    }
}