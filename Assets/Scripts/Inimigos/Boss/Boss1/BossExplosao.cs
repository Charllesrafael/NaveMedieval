using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class BossExplosao : MonoBehaviour
    {
        [Header("Shake")]
        Transform _camera;
        [SerializeField] private int numeroShaker;
        [SerializeField] private Vector2 distShake;
        [SerializeField] private float delayShake = 0.1f;
        Vector3 posInicial;

        [Header("Explosao")]
        public float delayExplosoes = 0.2f;
        public float delayExplosaoFinal = 0.2f;
        public float DelayFim = 2;
        public Action FimExplosoes;
        [SerializeField] private GameObject explosaoFinal;
        [SerializeField] private List<GameObject> explosoes;

        private void Awake()
        {
            _camera = Camera.main.gameObject.transform;
            posInicial = _camera.position;
        }

        public void Explodir()
        {
            StartCoroutine(IExplodir());
        }

        private IEnumerator IExplodir()
        {
            int current;
            while (explosoes.Count > 0)
            {
                current = UnityEngine.Random.Range(0, explosoes.Count);
                if (explosoes[current] != null)
                    explosoes[current].SetActive(true);
                explosoes.RemoveAt(current);
                StartCoroutine(ShakeCamera());
                ConfigurandoEfeitosBoss.instance.explosao?.Invoke();
                yield return new WaitForSeconds(delayExplosoes);
            }

            ConfigurandoEfeitosBoss.instance.flashExplosao?.Invoke();
            if (explosaoFinal != null)
            {
                explosaoFinal.SetActive(true);
                explosaoFinal.transform.parent = null;
            }

            yield return new WaitForSeconds(delayExplosaoFinal);
            FimExplosoes?.Invoke();
        }

        private IEnumerator ShakeCamera()
        {
            int t = 1;
            for (int i = 0; i < numeroShaker; i++)
            {
                _camera.position = posInicial + new Vector3(t * distShake.x, t * distShake.y, _camera.position.z);
                yield return new WaitForSeconds(delayShake);
                _camera.position = posInicial;
                t = -t;
            }
            _camera.position = posInicial;
        }
    }
}
