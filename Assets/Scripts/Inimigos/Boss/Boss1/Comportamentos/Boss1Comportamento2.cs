using System.Collections;
using System;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss1Comportamento2 : BossComportamento
    {

        [SerializeField] private GameObject particulaIndicandoLazerMaior;
        [SerializeField] private float tempoEsperaCarregarAtaqueLazer = 5f;
        [SerializeField] private GameObject LazerMaior;
        [SerializeField] private float tempoAtaqueLazer = 5f;

        [SerializeField] private BossMovimento bossMovimento;
        [SerializeField] private Boss boss;

        [SerializeField] private ArmaBase armas1;
        [SerializeField] private ArmaBase[] armas2;
        [SerializeField] private ArmaBase[] armas3;


        private bool atacandoComLazer = false;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        private float tempoEsperaCarregarAtaqueLazerAtual = 0;
        private float tempoAtaqueLazerAtual = 0;
        private Coroutine comportamento;


        public override void Ativar(Action onFimComportamento = null)
        {
            comportamento = StartCoroutine(ControlerComportamentos(onFimComportamento));
        }

        public override void Desativar()
        {
            if (comportamento != null)
            {
                StopCoroutine(comportamento);
                particulaIndicandoLazerMaior.gameObject.SetActive(false);
                LazerMaior.gameObject.SetActive(false);
            }
        }

        private IEnumerator ControlerComportamentos(Action onFimComportamento = null)
        {
            bossMovimento.idPonto = 1;
            bool fimComportamento = false;
            tempoEsperaCarregarAtaqueLazerAtual = 0;
            tempoAtaqueLazerAtual = 0;

            while (!fimComportamento && !boss.morto)
            {
                if (!atacandoComLazer)
                {
                    if (bossMovimento.GetIdPonto() != 1)
                        bossMovimento.SetIdPonto(1);

                    if (bossMovimento.TargetLonge(this.transform.position, 1))
                    {
                        armas1.Atirar();
                    }
                    else
                    {
                        Comportamto2();

                        if (!particulaIndicandoLazerMaior.gameObject.activeSelf)
                            particulaIndicandoLazerMaior.gameObject.SetActive(true);

                        if (tempoEsperaCarregarAtaqueLazerAtual < tempoEsperaCarregarAtaqueLazer)
                        {
                            tempoEsperaCarregarAtaqueLazerAtual += Time.deltaTime;
                        }
                        else
                        {
                            atacandoComLazer = true;
                            tempoEsperaCarregarAtaqueLazerAtual = 0;
                        }
                    }
                }
                else
                {
                    Comportamto3();

                    if (tempoAtaqueLazerAtual < tempoAtaqueLazer)
                    {
                        tempoAtaqueLazerAtual += Time.deltaTime;
                    }
                    else
                    {
                        atacandoComLazer = false;
                        fimComportamento = true;
                        LazerMaior.gameObject.SetActive(false);
                        particulaIndicandoLazerMaior.gameObject.SetActive(false);
                    }
                }
                yield return waitForEndOfFrame;
            }
            onFimComportamento?.Invoke();
        }

        private void Comportamto2()
        {
            if (!boss.rig.isKinematic)
            {
                particulaIndicandoLazerMaior.gameObject.SetActive(true);
                boss.rig.isKinematic = true;
            }

            foreach (var item in armas2)
                item.Atirar();
        }

        private void Comportamto3()
        {
            if (!LazerMaior.gameObject.activeSelf)
            {
                LazerMaior.gameObject.SetActive(true);
                particulaIndicandoLazerMaior.gameObject.SetActive(false);
            }

            foreach (var item in armas3)
                item.Atirar();
        }
    }
}
