using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class DroneExtraLazers : MonoBehaviour
    {
        [SerializeField] private float tempoExtraLazers;
        [SerializeField] private GameObject Lazer;
        private List<GameObject> Lazers;
        private PlayerVida player;

        IEnumerator Start()
        {
            player = GameManager.GetPlayer();
            Lazers = new List<GameObject>();
            CriarLazers();
            yield return new WaitForSeconds(tempoExtraLazers);
            GameManager.instance.temDrone = false;
            for (int i = Lazers.Count - 1; i >= 0 ; i--)
            {
                Destroy(Lazers[i].gameObject);
            }
            Destroy(this.gameObject);
        }

        private void CriarLazers()
        {
            ArmaProjetil armaProjetil = player.GetComponentInChildren<ArmaProjetil>();
            if(armaProjetil != null)
            {
                Transform[] pontos = armaProjetil.pontosLevel[2].pontos;

                foreach (var ponto in pontos)
                {
                    GameObject _lazer = ControllerPool.Create(Lazer, ponto.position, ponto.rotation);
                    _lazer.transform.parent = ponto;
                    Lazers.Add(_lazer);
                }
            }
        }
    }
}
