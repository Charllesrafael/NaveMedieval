using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ManagerCheat : MonoBehaviour
    {
        [SerializeField] private bool cheat = true;
        [SerializeField] bool pular_historia_cheat_code = true;
        [SerializeField] private IntRef _vidaAtual;

        private void Update()
        {
            if (!cheat || (LoadingManager.instance != null && LoadingManager.instance.loading))
                return;



            Imortalidade();
            PulaParaBoss();
            ChanceFase();
            Vida();
            ChangeNave();
        }

        private void PulaParaBoss()
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                VaiProBoss();
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                BossVida bossVida = FindObjectOfType<BossVida>();
                if(bossVida)bossVida.vidaAtual = 0;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                print("DeleteAll");
                PlayerPrefs.DeleteAll();
            }
        }

        private void VaiProBoss()
        {
            if(!FindObjectOfType<Boss>())
            {
                SpawnerSystem.instance.StopAllCoroutines();
                ControlerCenario.instance.ProximaCenario();
                var wave = SpawnerSystem.instance.spawnerSystemModel.waves[SpawnerSystem.instance.spawnerSystemModel.waves.Length - 1];
                SpawnerSystem.instance.StartCoroutine(SpawnerSystem.instance.CriarWave(wave));
            }
        }

        private void Imortalidade()
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                PlayerVida playerVida = FindObjectOfType<PlayerVida>();
                if (playerVida)
                    playerVida.vidaInicial = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                PlayerVida playerVida = FindObjectOfType<PlayerVida>();
                if (playerVida)
                    playerVida.vidaInicial = -1;
            }
        }

        private void ChanceFase()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ManagerScene.instance?.FaseAnterior(pular_historia_cheat_code);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ManagerScene.instance?.ProximaFase(pular_historia_cheat_code);
        }

        private static void Vida()
        {
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                PlayerVida playerVida = FindObjectOfType<PlayerVida>();
                if (playerVida._vidaAtual.value < 3)
                    playerVida._vidaAtual.value++;
                else if (playerVida._vidaAtual.value == playerVida.vidaMax)
                    playerVida.ObterDrone();

                ArmasUI.ChangeLive(playerVida._vidaAtual.value);
            }

            if (Input.GetKeyDown(KeyCode.Minus))
            {
                PlayerVida playerVida = FindObjectOfType<PlayerVida>();
                if (playerVida._vidaAtual.value > 0)
                    playerVida._vidaAtual.value--;

                ArmasUI.ChangeLive(playerVida._vidaAtual.value);
            }
        }

        private void ChangeNave()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelecaoNave(false);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                SelecaoNave(true);
        }

        private void SelecaoNave(bool proximo)
        {
            VisualNaveControler visualNaveControler = FindObjectOfType<VisualNaveControler>();

            int idSelecao = visualNaveControler._personagemSelecionado;

            idSelecao += proximo ? 1 : -1;
            idSelecao = (idSelecao > ManagerScene.instance.totemData.listaNaves.Count -1) ? 0 : idSelecao;
            idSelecao = (idSelecao < 0) ? ManagerScene.instance.totemData.listaNaves.Count -1 : idSelecao;

            visualNaveControler._personagemSelecionado = idSelecao;
            visualNaveControler.CriaNave(idSelecao);

            NaveData naveData = ManagerScene.instance.GetNave(idSelecao);

            FindObjectOfType<ControlerTiro>()?.SetArma(naveData.GetArmaId());
        }
    }
}
