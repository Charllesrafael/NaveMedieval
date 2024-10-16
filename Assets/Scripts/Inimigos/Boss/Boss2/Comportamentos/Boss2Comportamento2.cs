using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss2Comportamento2 : BossComportamento
    {
        [SerializeField] private Boss2AnimatorController AnimatorController;
        [SerializeField] private float dalayTempo = 1;
        [SerializeField] private ArmaBase[] Armas;

        private WaitForSeconds waitForSeconds;
        private Coroutine comportamento;


        private void Start()
        {
            waitForSeconds = new WaitForSeconds(dalayTempo);
        }

        public override void Ativar(Action onFimComportamento = null)
        {
            AnimatorController.SetTrigger("idle");
            AnimatorController.callBack = () =>
            {
                comportamento = StartCoroutine(TempoComportamento(onFimComportamento));
            };
        }

        private IEnumerator TempoComportamento(Action onFimComportamento)
        {
            StartCoroutine(ControleArmas());
            yield return waitForSeconds;
            onFimComportamento?.Invoke();
            Desativar();
        }

        private IEnumerator ControleArmas()
        {
            while (true)
            {
                foreach (var item in Armas)
                {
                    item.Atirar();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        public override void Desativar()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            Desativar();
        }
    }
}
