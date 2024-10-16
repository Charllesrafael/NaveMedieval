using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss2Comportamento3 : BossComportamento
    {
        [SerializeField] private Boss2AnimatorController AnimatorController;
        [SerializeField] private float dalayTempo = 1;
        [SerializeField] private float dalayEntreTentaculos = 1;
        [SerializeField] private ArmaBase[] Armas;

        private WaitForSeconds waitForSeconds;
        private WaitForSeconds waitForSecondsTentaculos;
        private Coroutine comportamento;


        private void Start()
        {
            waitForSeconds = new WaitForSeconds(dalayTempo);
            waitForSecondsTentaculos = new WaitForSeconds(dalayEntreTentaculos);
        }

        public override void Ativar(Action onFimComportamento = null)
        {
            AnimatorController.SetTrigger("tentaculo");
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
                    yield return waitForSecondsTentaculos;
                }
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
