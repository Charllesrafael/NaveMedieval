using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class Boss2Comportamento1 : BossComportamento
    {
        [SerializeField] private Boss2AnimatorController AnimatorController;
        [SerializeField] private float dalayTempo = 1;
        [SerializeField] private GameObject tentaculos;
        [SerializeField] private ArmaBase[] Armas;
        [SerializeField] private GameObject[] pontosTentaculos;

        private WaitForSeconds waitForSeconds;
        private Coroutine comportamento;


        private void Start()
        {
            waitForSeconds = new WaitForSeconds(dalayTempo);
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
            foreach (var item in pontosTentaculos)
            {
                Comportamento(item);
                yield return waitForSeconds;
            }

            yield return waitForSeconds;
            onFimComportamento?.Invoke();
            Desativar();
        }

        private void Comportamento(GameObject item)
        {
            ControllerPool.Create(tentaculos, item.transform.position, item.transform.rotation).transform.parent = item.transform;
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
